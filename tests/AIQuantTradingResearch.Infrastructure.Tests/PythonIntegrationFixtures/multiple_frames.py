import json
import sys

request = json.loads(sys.stdin.buffer.read().decode("utf-8-sig"))
response = {"contractVersion": 1, "status": "success", "correlationId": request["correlationId"], "result": {"status": "available"}}
json.dump(response, sys.stdout)
json.dump(response, sys.stdout)
