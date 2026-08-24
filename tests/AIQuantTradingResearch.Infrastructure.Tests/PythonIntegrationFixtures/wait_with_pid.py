import json
import os
import sys
import time

request = json.loads(sys.stdin.buffer.read().decode("utf-8-sig"))
with open(request["payload"]["pidPath"], "w", encoding="utf-8") as handle:
    handle.write(str(os.getpid()))
    handle.flush()
time.sleep(30)
