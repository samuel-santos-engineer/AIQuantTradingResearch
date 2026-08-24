import json
import sys

request = json.loads(sys.stdin.buffer.read().decode("utf-8-sig"))
json.dump({"contractVersion": 2, "status": "success", "correlationId": request["correlationId"], "result": {"status": "available"}}, sys.stdout)
