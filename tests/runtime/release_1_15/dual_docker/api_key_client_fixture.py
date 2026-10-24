"""WP02 fixture only; no production D4 parsing or analytics."""
import base64
import http.client
import json
import ssl
import socket
import threading
import time
from urllib.parse import urlsplit


class Unavailable(Exception):
    """Sanitized boundary error; never expose URLs, headers or response text."""


def key_bytes(key):
    if not isinstance(key, str) or len(key) != 44:
        raise Unavailable("Request unavailable.")
    try:
        raw = base64.b64decode(key, validate=True)
    except (ValueError, TypeError):
        raise Unavailable("Request unavailable.") from None
    if len(raw) != 32 or base64.b64encode(raw).decode("ascii") != key:
        raise Unavailable("Request unavailable.")
    return raw


class Client:
    def __init__(self, url, key, ca_file=None, timeout=4.0, limit=4096):
        try:
            target = urlsplit(url)
            if (target.scheme != "https" or not target.hostname or target.username is not None
                    or target.password is not None or target.query or target.fragment
                    or target.path not in ("", "/") or any(c.isspace() for c in url)):
                raise ValueError()
            port = target.port or 443
            key_bytes(key)
            if not 0 < timeout <= 4 or not 0 < limit <= 4096:
                raise ValueError()
            self.host, self.port, self.key = target.hostname, port, key
            self.timeout, self.limit = timeout, limit
            self.context = ssl.create_default_context(cafile=ca_file)
            self.last_known = None
            self.connected = False
        except Exception:
            raise Unavailable("Request unavailable.") from None

    def refresh(self):
        connection = None
        watchdog = None
        response = None
        started = time.monotonic()
        def remaining():
            budget = self.timeout - (time.monotonic() - started)
            if budget <= 0:
                raise TimeoutError()
            return budget

        try:
            # Standard synchronous DNS/TCP/TLS: native DNS may outlast socket timeout.
            # Direct connection: no environment proxy, cookies, redirects or browser key.
            connection = http.client.HTTPSConnection(self.host, self.port, timeout=remaining(),
                                                     context=self.context)
            connection.connect()
            transport = connection.sock
            transport.settimeout(remaining())
            connection.request("GET", "/fixture/status", headers={"X-API-Key": self.key})
            budget = remaining()
            transport.settimeout(budget)
            def interrupt():
                try:
                    transport.shutdown(socket.SHUT_RDWR)
                except OSError:
                    pass
            # Bound a peer that drips headers or bytes faster than idle timeout.
            watchdog = threading.Timer(budget, interrupt)
            watchdog.daemon = True
            watchdog.start()
            response = connection.getresponse()
            if response.status != 200 or response.getheader("Content-Type", "").split(";")[0] != "application/json":
                raise ValueError()
            if response.getheader("Content-Length") is not None and int(response.getheader("Content-Length")) > self.limit:
                raise ValueError()
            # read1 returns after one buffered/raw read; slow chunks cannot reset deadline.
            data = bytearray()
            while True:
                transport.settimeout(remaining())
                chunk = response.read1(min(1024, self.limit + 1 - len(data)))
                if not chunk:
                    break
                data.extend(chunk)
                if len(data) > self.limit:
                    raise ValueError()
            if response.length not in (None, 0):
                raise ValueError()
            value = json.loads(data.decode("utf-8"), object_pairs_hook=_unique)
            if (type(value) is not dict or set(value) != {"fixture", "status"}
                    or value["fixture"] is not True or value["status"] != "unavailable"):
                raise ValueError()
            remaining()  # Never publish a result that has already consumed the budget.
            self.last_known, self.connected = value, True
            return value
        except Exception:
            self.connected = False
            raise Unavailable("Request unavailable.") from None
        finally:
            if watchdog is not None:
                watchdog.cancel()
            if response is not None:
                response.close()
            if connection is not None:
                connection.close()


def _unique(pairs):
    value = {}
    for key, item in pairs:
        if key in value:
            raise ValueError()
        value[key] = item
    return value
