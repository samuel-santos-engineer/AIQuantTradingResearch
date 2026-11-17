"""Synthetic inspect fixtures only."""
import contextlib
import io
import json
import os
from pathlib import Path
import shutil
import subprocess
import tempfile
import unittest
from unittest.mock import patch
import wp65_local_image_metadata as m


def fixture():
    return {"Id": m.WP61, "Os": "linux", "Architecture": "amd64", "User": "12002:12002", "ExposedPorts": {"8501/tcp": {}},
            "Entrypoint": ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py", "--server.port=8501", "--server.address=0.0.0.0"], "Cmd": [], "RepoDigests": []}


class Tests(unittest.TestCase):
    def test_exact_id_command_and_no_rich_fields(self):
        command = m.command("desktop-linux")
        self.assertEqual(["docker", "--context", "desktop-linux", "image", "inspect", m.WP61], command[:6])
        for name in ["Env", "Labels", "History", "Mounts", "RootFS"]: self.assertNotIn(name, command[-1])
        with self.assertRaises(ValueError): m.command("unexpected-context")

    def test_matching_contract_leaves_digest_kinds_unknown(self):
        r = m.project(json.dumps(fixture()).encode())
        self.assertEqual("MATCH", r["result"])
        self.assertEqual("UNKNOWN", r["ociIndexDigest"])
        self.assertEqual("UNKNOWN", r["configDigest"])
        self.assertEqual("UNVERIFIED", r["publication"])

    def test_wrong_id_malformed_duplicate_and_cap(self):
        d = fixture(); d["Id"] = "sha256:" + "0" * 64
        for raw in [json.dumps(d).encode(), b"{", b"[]", b'{"Id":1,"Id":2}', b"x" * (m.LIMIT+1)]:
            self.assertEqual("UNVERIFIED", m.project(raw)["result"])

    def test_contract_mismatch_not_identity_equivalence(self):
        d = fixture(); d["User"] = "0"
        r = m.project(json.dumps(d).encode())
        self.assertEqual("MISMATCH", r["result"])
        self.assertEqual("UNKNOWN", r["runtimeManifestDigest"])

    def test_args_values_not_echoed(self):
        d = fixture(); d["Cmd"] = ["fake-sensitive-command-value"]
        r = json.dumps(m.project(json.dumps(d).encode()))
        self.assertNotIn("fake-sensitive", r)
        self.assertNotIn('"Entrypoint":', r)
        self.assertNotIn("realtime_financial_visualization.py", r)

    def test_safe_full_repodigest_and_unsafe_ref(self):
        d = fixture(); d["RepoDigests"] = ["ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:"+"1"*64]
        self.assertEqual(d["RepoDigests"], m.project(json.dumps(d).encode())["safeRepoDigests"])
        d["RepoDigests"] = ["https://fake-user:fake-secret@private.invalid/image"]
        r = m.project(json.dumps(d).encode()); self.assertEqual("UNVERIFIED", r["result"])
        self.assertNotIn("fake-secret", json.dumps(r))

    def test_default_no_daemon_or_network(self):
        with patch("subprocess.Popen", side_effect=AssertionError("no daemon")), patch("socket.socket", side_effect=AssertionError("no network")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(0, m.main([]))
        self.assertEqual(0, json.loads(output.getvalue())["dockerOperations"])


GO_HARNESS = r"""
package main
import (
 "bytes"
 "encoding/json"
 "fmt"
 "os"
 "text/template"
)
type Fixture struct { Name string; Root json.RawMessage; OriginalError bool; Expected map[string]interface{} }
type Input struct { Original string; Corrected string; Fixtures []Fixture }
func render(source string, root interface{}) ([]byte,error) {
 funcs:=template.FuncMap{"json":func(value interface{})(string,error){ b,e:=json.Marshal(value);return string(b),e }}
 t,e:=template.New("projection").Option("missingkey=error").Funcs(funcs).Parse(source)
 if e!=nil{return nil,e};var out bytes.Buffer;e=t.Execute(&out,root);return out.Bytes(),e
}
func main(){
 var input Input
 if json.NewDecoder(os.Stdin).Decode(&input)!=nil { fmt.Println(`{"result":"INPUT_ERROR"}`);os.Exit(1) }
 failures:=0;oldFailures:=0
 for _,f:=range input.Fixtures{
  var root interface{}
  if json.Unmarshal(f.Root,&root)!=nil { failures++;continue }
  _,oldError:=render(input.Original,root)
  if oldError!=nil{oldFailures++}
  if (oldError!=nil)!=f.OriginalError {failures++}
  output,e:=render(input.Corrected,root)
  if e!=nil { failures++;continue }
  var actual map[string]interface{}
  if json.Unmarshal(output,&actual)!=nil{failures++;continue}
  a,_:=json.Marshal(actual);want,_:=json.Marshal(f.Expected)
  if !bytes.Equal(a,want){failures++}
  if bytes.Contains(output,[]byte("FORBIDDEN_ENV_VALUE")) || bytes.Contains(output,[]byte("FORBIDDEN_LABEL_VALUE")){failures++}
 }
 // Representative typed root: no assumed Config/ID alias; corrected template must return nulls safely.
 type TypedRoot struct { ID string; Config interface{} }
 typed:=TypedRoot{ID:"synthetic-typed-id",Config:nil}
 output,e:=render(input.Corrected,typed)
 if e!=nil{failures++} else {
  var value map[string]interface{};if json.Unmarshal(output,&value)!=nil{failures++}
  if len(value)!=8{failures++};for _,v:=range value{if v!=nil{failures++}}
 }
 result:=map[string]interface{}{"evaluated":len(input.Fixtures)+1,"failures":failures,"originalFailureCases":oldFailures}
 json.NewEncoder(os.Stdout).Encode(result)
 if failures!=0{os.Exit(1)}
}
"""


def go_toolchain():
    found = shutil.which("go")
    if found: return found
    for name in ["C:/Program Files/Go/bin/go.exe", "C:/Go/bin/go.exe"]:
        if Path(name).is_file(): return name
    return None


ORIGINAL_FORMAT = ('{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}},'
                   '"User":{{json .Config.User}},"ExposedPorts":{{json .Config.ExposedPorts}},'
                   '"Entrypoint":{{json .Config.Entrypoint}},"Cmd":{{json .Config.Cmd}},"RepoDigests":{{json .RepoDigests}}}')


def go_fixtures():
    flat = fixture()
    config = {key: flat[key] for key in ["User", "ExposedPorts", "Entrypoint", "Cmd"]}
    root = {key: flat[key] for key in ["Id", "Os", "Architecture", "RepoDigests"]}
    root["Config"] = config
    root["Env"] = ["FORBIDDEN_ENV_VALUE"]
    root["Labels"] = {"ignored": "FORBIDDEN_LABEL_VALUE"}
    cases = [{"Name": "normal", "Root": root, "OriginalError": False, "Expected": flat}]
    def add(name, value, expected, old_error):
        cases.append({"Name": name, "Root": value, "Expected": expected, "OriginalError": old_error})
    nulls = {key: None for key in flat}
    for name, value in [("absent-config", None), ("nil-config", None), ("wrong-config-type", "wrong-type")]:
        value_root = dict(root)
        if name == "absent-config": value_root.pop("Config")
        else: value_root["Config"] = value
        expected = {key: flat[key] for key in ["Id", "Os", "Architecture", "RepoDigests"]}
        expected.update({key: None for key in config})
        add(name, value_root, expected, True)
    for name in ["Cmd", "Entrypoint", "ExposedPorts", "User"]:
        value_root = dict(root, Config=dict(config));value_root["Config"].pop(name)
        expected = dict(flat);expected[name] = None
        add("missing-" + name, value_root, expected, True)
    for name in ["Cmd", "Entrypoint", "ExposedPorts", "User"]:
        value_root = dict(root, Config=dict(config));value_root["Config"][name] = None
        expected = dict(flat);expected[name] = None
        add("nil-" + name, value_root, expected, False)
    for name in ["Id", "Os", "Architecture", "RepoDigests"]:
        value_root = dict(root);value_root.pop(name)
        expected = dict(flat);expected[name] = None
        add("missing-" + name, value_root, expected, True)
    for name, value in [("Cmd", False), ("User", 12002), ("Entrypoint", "not-an-array")]:
        value_root = dict(root, Config=dict(config));value_root["Config"][name] = value
        expected = dict(flat);expected[name] = value
        add("wrong-type-" + name, value_root, expected, False)
    add("non-object-root", [], nulls, True)
    return cases


class OfflineTemplateTests(unittest.TestCase):
    def test_parser_optional_and_wrong_types_fail_closed(self):
        for name, values in {"Cmd": [False, "", {}, 1], "Entrypoint": [None, False, ""], "User": [None, False, 12002], "Os": [None, 1], "Architecture": [None, []], "ExposedPorts": [None, [], ""], "RepoDigests": [None, {}, ""]}.items():
            for value in values:
                data=fixture();data[name]=value
                self.assertEqual("UNVERIFIED", m.project(json.dumps(data).encode())["result"])
        data=fixture();data["Cmd"]=None
        self.assertEqual("MATCH", m.project(json.dumps(data).encode())["result"])

    def test_flat_config_is_not_silently_assumed(self):
        data=fixture()
        for name in ["User", "ExposedPorts", "Entrypoint", "Cmd"]:data[name]=None
        self.assertEqual("UNVERIFIED", m.project(json.dumps(data).encode())["result"])

    def test_invalid_port_names_do_not_escape(self):
        for name in ["0/tcp", "99999/tcp", "fake-sensitive-path"]:
            data=fixture();data["ExposedPorts"]={name:{}}
            result=m.project(json.dumps(data).encode())
            self.assertEqual("UNVERIFIED", result["result"])
            self.assertNotIn("fake-sensitive", json.dumps(result))

    def test_harness_is_self_contained_and_template_allowlisted(self):
        self.assertIn('"text/template"', GO_HARNESS)
        self.assertNotIn('"net/http"', GO_HARNESS)
        for name in ["Env", "Labels", "History", "RootFS"]:self.assertNotIn(name,m.FORMAT)
        self.assertFalse(m.TEMPLATE_GO_EVALUATED)

    @unittest.skipUnless(go_toolchain(), "GO_TOOLCHAIN_UNAVAILABLE: actual text/template evaluation NOT RUN")
    def test_actual_go_text_template_evaluation(self):
        # Offline only: installed compiler and standard library; downloads/toolchain switching disabled.
        with tempfile.TemporaryDirectory(prefix="aiq-wp65-go-template-") as folder:
            root=Path(folder);source=root/"main.go";source.write_text(GO_HARNESS,encoding="utf-8",newline="\n")
            env=dict(os.environ);env.update(GOPROXY="off",GOSUMDB="off",GOTOOLCHAIN="local",GO111MODULE="off",CGO_ENABLED="0",GOTELEMETRY="off",GOCACHE=str(root/"cache"))
            proc=subprocess.run([go_toolchain(),"run",str(source)],input=json.dumps({"Original":ORIGINAL_FORMAT,"Corrected":m.FORMAT,"Fixtures":go_fixtures()}).encode(),capture_output=True,timeout=90,env=env)
            self.assertEqual(0,proc.returncode,"Go harness failed; raw fixture/error output withheld")
            result=json.loads(proc.stdout)
            self.assertEqual(0,result["failures"])
            self.assertGreater(result["originalFailureCases"],0)
            self.assertEqual(len(go_fixtures())+1,result["evaluated"])


if __name__ == "__main__": unittest.main()
