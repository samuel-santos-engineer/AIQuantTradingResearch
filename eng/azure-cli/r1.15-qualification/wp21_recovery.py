"""Independent recovery: no import of or dependency on controller executable."""
import argparse
import json
import os
from pathlib import Path
import wp21_runtime as R


def main(argv=None):
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--recover",action="store_true")
    args=parser.parse_args(argv)
    if not args.recover:
        print(json.dumps({"result":"RECOVERY_NOT_RUN","PUBLIC_RUNTIME_VERIFIED":False}));return 0
    try:
        directory=Path(os.environ["AIQ_WP21_ATTEMPT_DIRECTORY"])
        R.private_directory(directory)
        value=R.recover(directory,R.Azure());print(json.dumps(value))
        return 0 if value["rollbackVerified"] else 1
    except BaseException as error:
        reason=str(error) if isinstance(error,R.Blocked) else "LOCAL_OR_OPERATION_FAILURE_REVIEW_PRIVATE_EVIDENCE"
        print(json.dumps({"result":"CRITICAL_RECOVERY_UNVERIFIED","reason":reason,"PUBLIC_RUNTIME_VERIFIED":False}));return 1


if __name__=="__main__":raise SystemExit(main())
