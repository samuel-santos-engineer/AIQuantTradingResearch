"""Owned local Linux Docker qualification. No Azure, provider or registry operations."""
import base64
import http.client
import json
import os
from pathlib import Path
import socket
import ssl
import subprocess
import time
import unittest

from api_key_client_fixture import Client, Unavailable
from tls_fixture import create

KEY = base64.b64encode(b"A" * 32).decode("ascii")
FORMER_KEY = base64.b64encode(b"B" * 32).decode("ascii")
WRONG = base64.b64encode(b"C" * 32).decode("ascii")
PRIVATE_CANARY = "WP02_PRIVATE_PROVIDER_HMAC_CANARY"


def command(args, timeout=60):
    result = subprocess.run(args, capture_output=True, text=True, encoding="utf-8", errors="replace", timeout=timeout)
    if result.returncode:
        raise RuntimeError("Owned fixture command failed: " + args[0] + " " + args[1] + "\n" + result.stderr[-3000:])
    return result.stdout.strip()


class PairTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.run_id = os.environ["AIQ_WP02_RUN_ID"]
        cls.output = Path(os.environ["AIQ_WP02_OUTPUT"]).resolve()
        cls.research = os.environ["AIQ_WP02_RESEARCH_IMAGE"]
        cls.ui = os.environ["AIQ_WP02_UI_IMAGE"]
        cls.owned = []
        cls.network = "aiq-wp02-" + cls.run_id
        cls.certdir = cls.output / "certs"
        cls.started = time.monotonic()
        try:
            command(["docker", "network", "create", "--label", "aiq.wp02=" + cls.run_id, cls.network])
            create(cls.research, cls.certdir, cls.run_id)
            cls.trust = str(cls.certdir / "trust.pem")
            cls.main_id, cls.port = cls.start_research(alias="research")
            cls.url = "https://localhost:" + str(cls.port)
            cls.ui_id = cls.start_ui()
        except Exception:
            cls.tearDownClass()
            raise

    @classmethod
    def launch(cls, image, env, mounts, alias=None):
        args = ["docker", "run", "-d", "--read-only", "--cap-drop=ALL", "--security-opt", "no-new-privileges", "--cpus=1", "--memory=512m", "--pids-limit=64", "--stop-timeout=8", "--label", "aiq.wp02=" + cls.run_id, "--network", cls.network, "--publish", "127.0.0.1::" + ("52731" if image == cls.research else "8501"), "--tmpfs", "/tmp:rw,noexec,nosuid,size=32m"]
        if image == cls.research:
            args += ["--tmpfs", "/private:rw,noexec,nosuid,uid=12001,gid=12001,mode=0700,size=1m"]
        else:
            args += ["--tmpfs", "/ui:rw,noexec,nosuid,uid=12002,gid=12002,mode=0700,size=16m"]
        if alias:
            args += ["--network-alias", alias]
        for mount in mounts:
            args += ["--mount", mount]
        for key, value in env.items():
            args += ["--env", key + "=" + value]
        identifier = command(args + [image])
        cls.owned.append(identifier)
        return identifier

    @classmethod
    def port_for(cls, identifier, port):
        value = command(["docker", "port", identifier, str(port) + "/tcp"])
        return int(value.rsplit(":", 1)[1])

    @classmethod
    def start_research(cls, active=KEY, alias=None, delay_stage=None):
        env = {"AIQ_FIXTURE_ACTIVE_KEY": active, "FIXTURE_PROVIDER_CANARY": PRIVATE_CANARY, "FIXTURE_HMAC_CANARY": PRIVATE_CANARY}
        if delay_stage:
            env.update(AIQ_FIXTURE_STATUS_DELAY_MS="800", AIQ_FIXTURE_DELAY_STAGE=delay_stage)
        identifier = cls.launch(cls.research, env, ["type=bind,source=" + str(cls.certdir) + ",target=/certs,readonly"], alias=alias)
        port = cls.port_for(identifier, 52731)
        until = time.monotonic() + 30
        while time.monotonic() < until:
            try:
                if cls.request(port, "/health/live", None)[0] == 200:
                    command(["docker", "exec", identifier, "sh", "-c", "umask 0077; for f in raw.sqlite raw.sqlite-wal checkpoint watermark config.json hmac.key; do printf WP02_PRIVATE_PROVIDER_HMAC_CANARY > /private/$f; done"])
                    return identifier, port
            except (OSError, http.client.HTTPException):
                pass
            time.sleep(0.2)
        raise RuntimeError("Fixture startup deadline exceeded")

    @classmethod
    def start_ui(cls):
        return cls.launch(cls.ui, {"AIQ_FIXTURE_RESEARCH_URL": "https://research:52731", "AIQ_FIXTURE_ACTIVE_KEY": KEY, "AIQ_FIXTURE_TRUST": "/trust.pem"}, ["type=bind,source=" + cls.trust + ",target=/trust.pem,readonly"])

    @classmethod
    def request(cls, port, path="/fixture/status", key=KEY, body=None, method="GET"):
        connection = http.client.HTTPSConnection("localhost", port, context=ssl.create_default_context(cafile=cls.trust), timeout=4)
        try:
            headers = {} if key is None else {"X-API-Key": key}
            connection.request(method, path, body=body, headers=headers)
            response = connection.getresponse()
            return response.status, response.read(8192)
        finally:
            connection.close()

    @classmethod
    def tearDownClass(cls):
        records = []
        for identifier in reversed(getattr(cls, "owned", [])):
            try:
                info = json.loads(command(["docker", "inspect", identifier]))[0]
                if info["Config"]["Labels"].get("aiq.wp02") != cls.run_id:
                    raise RuntimeError("Ownership mismatch; no cleanup")
                log_result = subprocess.run(["docker", "logs", identifier], capture_output=True, text=True, timeout=10)
                logs = log_result.stdout + log_result.stderr
                if any(secret in logs for secret in [KEY, FORMER_KEY, WRONG, PRIVATE_CANARY]):
                    raise RuntimeError("Fixture log leakage")
                command(["docker", "stop", "--time", "8", identifier], timeout=15)
                state = json.loads(command(["docker", "inspect", "--format", "{{json .State}}", identifier]))
                records.append({"id": identifier, "exit": state["ExitCode"], "oom": state["OOMKilled"]})
                command(["docker", "rm", identifier])
            except Exception as error:
                records.append({"id": identifier, "error": type(error).__name__})
        if getattr(cls, "network", None):
            try:
                command(["docker", "network", "rm", cls.network])
            except Exception as error:
                records.append({"network_cleanup": type(error).__name__})
        if getattr(cls, "output", None):
            (cls.output / "cleanup.json").write_text(json.dumps(records, indent=2) + "\n", encoding="utf-8")
        if any("error" in r or "network_cleanup" in r or r.get("oom") for r in records):
            raise AssertionError("Owned fixture cleanup/leak/OOM check failed")

    def test_anonymous_wrong_and_cross_environment_denied(self):
        for key in [None, "", WRONG, "x" * 44, KEY + "," + FORMER_KEY]:
            status, body = self.request(self.port, key=key)
            self.assertEqual(status, 401)
            self.assertEqual(body, b"Request unavailable.")

    def test_authenticated_status_and_no_raw_routes(self):
        self.assertEqual(Client(self.url, KEY, self.trust).refresh(), {"fixture": True, "status": "unavailable"})
        for path in ["/raw", "/private", "/admin", "/ingest", "/config", "/metrics"]:
            self.assertEqual(self.request(self.port, path)[0], 404)
            self.assertEqual(self.request(self.port, path, None)[0], 401)

    def test_duplicate_header_rejected(self):
        c = http.client.HTTPSConnection("localhost", self.port, context=ssl.create_default_context(cafile=self.trust), timeout=4)
        try:
            c.putrequest("GET", "/fixture/status")
            c.putheader("X-API-Key", KEY)
            c.putheader("X-API-Key", FORMER_KEY)
            c.endheaders()
            self.assertEqual(c.getresponse().status, 401)
        finally:
            c.close()

    def test_tls_chain_hostname_and_plaintext_negatives(self):
        with self.assertRaises(Unavailable):
            Client(self.url, KEY).refresh()
        with socket.create_connection(("localhost", self.port), timeout=4) as sock:
            with self.assertRaises(ssl.SSLCertVerificationError):
                ssl.create_default_context(cafile=self.trust).wrap_socket(sock, server_hostname="wrong-host.invalid")
        with self.assertRaises(Unavailable):
            Client("http://localhost:" + str(self.port), KEY)
        plain = http.client.HTTPConnection("localhost", self.port, timeout=4)
        try:
            try:
                plain.request("GET", "/health/live")
                self.assertNotEqual(plain.getresponse().status, 200)
            except (OSError, http.client.HTTPException):
                pass
        finally:
            plain.close()

    def test_request_bounds_and_invalid_utf8(self):
        for data, expected in [(b"{}", 503), (b'{"raw":1}', 400), (b"\xff", 400), (b"x" * 4097, 413)]:
            self.assertEqual(self.request(self.port, "/v1/research/snapshot", body=data, method="POST")[0], expected)
        self.assertEqual(self.request(self.port, "/v1/research/snapshot", None, b"x" * 4097, "POST")[0], 401)

    def test_image_environment_filesystem_and_identity_isolation(self):
        for identifier, uid in [(self.main_id, 12001), (self.ui_id, 12002)]:
            state = command(["docker", "exec", identifier, "cat", "/proc/1/status"])
            self.assertIn("Uid:\t" + "\t".join([str(uid)] * 4), state)
            self.assertIn("CapEff:\t0000000000000000", state)
            self.assertIn("CapBnd:\t0000000000000000", state)
            self.assertIn("NoNewPrivs:\t1", state)
        code = "import os; assert os.getuid()==12002; assert not os.path.exists('/private'); assert not os.path.exists('/certs'); assert not any('PROVIDER' in k or 'HMAC' in k for k in os.environ); assert not os.path.exists('/usr/share/dotnet'); print('isolation-pass')"
        self.assertEqual(command(["docker", "exec", self.ui_id, "/opt/venv/bin/python", "-B", "-c", code]), "isolation-pass")
        private = command(["docker", "exec", self.main_id, "stat", "-c", "%a %u %g", "/private", "/private/raw.sqlite", "/private/hmac.key"])
        self.assertEqual(private.splitlines(), ["700 12001 12001", "600 12001 12001", "600 12001 12001"])
        denied = "from pathlib import Path; p=Path('/private/raw.sqlite'); assert not p.exists(); assert not Path('/certs/server.pfx').exists(); print('private-files-absent')"
        self.assertEqual(command(["docker", "exec", self.ui_id, "/opt/venv/bin/python", "-B", "-c", denied]), "private-files-absent")
        image = json.loads(command(["docker", "image", "inspect", self.ui]))[0]
        self.assertNotIn(PRIVATE_CANARY, json.dumps(image))
        self.assertNotIn(KEY, json.dumps(image))
        self.assertNotIn(KEY, command(["docker", "history", "--no-trunc", self.ui]))

    def test_invalid_configuration_fails_closed(self):
        for env in [{}, {"AIQ_FIXTURE_ACTIVE_KEY": "invalid"},
                    {"AIQ_FIXTURE_ACTIVE_KEY": KEY, "AIQ_FIXTURE_PREVIOUS_KEY": FORMER_KEY},
                    {"AIQ_FIXTURE_ACTIVE_KEY": KEY, "AIQ_FIXTURE_PREVIOUS_EXPIRES": "obsolete"}]:
            identifier = self.launch(self.research, env, [])
            self.assertEqual(command(["docker", "wait", identifier], timeout=10), "64")
            self.assertNotIn(KEY, command(["docker", "logs", identifier]))

    def test_single_key_replacement_after_service_restart(self):
        first, first_port = self.start_research(active=FORMER_KEY)
        self.assertEqual(self.request(first_port, key=FORMER_KEY)[0], 200)
        self.assertEqual(self.request(first_port, key=KEY)[0], 401)
        command(["docker", "stop", "--time", "8", first], timeout=15)
        self.assertEqual(command(["docker", "inspect", "--format", "{{.State.Running}}", first]), "false")
        second, second_port = self.start_research(active=KEY)
        self.assertEqual(self.request(second_port, key=KEY)[0], 200)
        self.assertEqual(self.request(second_port, key=FORMER_KEY)[0], 401)
        command(["docker", "stop", "--time", "8", second], timeout=15)

    def test_local_delayed_headers_and_body_timeout_then_recover(self):
        observations = []
        for stage in ["headers", "body"]:
            identifier, port = self.start_research(delay_stage=stage)
            client = Client("https://localhost:" + str(port), KEY, self.trust)
            client.refresh()
            old = client.last_known
            client.timeout = 0.2
            started = time.monotonic()
            with self.assertRaises(Unavailable):
                client.refresh()
            elapsed = time.monotonic() - started
            self.assertLess(elapsed, 1.5, "Bounded local socket timeout exceeded allowance")
            self.assertIs(client.last_known, old)
            self.assertFalse(client.connected)
            client.timeout = 4
            client.refresh()
            self.assertTrue(client.connected)
            observations.append({"stage": stage, "timeoutSeconds": 0.2, "observedSeconds": elapsed})
            command(["docker", "stop", "--time", "8", identifier], timeout=15)
        (self.output / "delay-observations.json").write_text(json.dumps(observations, indent=2) + "\n", encoding="utf-8")

    def test_ui_real_render_connected_then_disconnected_and_peer_lifecycle(self):
        code = "from streamlit.testing.v1 import AppTest; a=AppTest.from_file('/app/public_app_fixture.py').run(timeout=20); assert not a.exception; assert any('connected' in x.value for x in a.info); print('connected-render-pass')"
        self.assertIn("connected-render-pass", command(["docker", "exec", self.ui_id, "/opt/venv/bin/python", "-B", "-c", code], timeout=30))
        probe = "from api_key_client_fixture import Client,Unavailable; import os; c=Client(os.environ['AIQ_FIXTURE_RESEARCH_URL'],os.environ['AIQ_FIXTURE_ACTIVE_KEY'],os.environ['AIQ_FIXTURE_TRUST']); print(c.refresh()['status'])"
        self.assertEqual(command(["docker", "exec", self.ui_id, "/opt/venv/bin/python", "-B", "-c", probe]), "unavailable")
        metrics = command(["docker", "stats", "--no-stream", "--format", "{{json .}}", self.main_id, self.ui_id], timeout=15)
        (self.output / "resource-samples.jsonl").write_text(metrics + "\n", encoding="utf-8")
        command(["docker", "stop", "--time", "8", self.main_id], timeout=15)
        code = "from streamlit.testing.v1 import AppTest; a=AppTest.from_file('/app/public_app_fixture.py').run(timeout=20); assert not a.exception; assert any('Disconnected' in x.value for x in a.warning); print('disconnected-render-pass')"
        self.assertIn("disconnected-render-pass", command(["docker", "exec", self.ui_id, "/opt/venv/bin/python", "-B", "-c", code], timeout=30))
        self.assertEqual(command(["docker", "inspect", "--format", "{{.State.Running}}", self.ui_id]), "true")
        # Independent fresh research remains alive after UI shutdown.
        identifier, port = self.start_research()
        command(["docker", "stop", "--time", "8", self.ui_id], timeout=15)
        self.assertEqual(self.request(port, key=KEY)[0], 200)
        self.assertEqual(command(["docker", "inspect", "--format", "{{.State.Running}}", identifier]), "true")


if __name__ == "__main__":
    unittest.main(verbosity=2)
