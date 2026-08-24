import sys

sys.stdin.buffer.read()
print("bounded test diagnostic", file=sys.stderr)
raise SystemExit(7)
