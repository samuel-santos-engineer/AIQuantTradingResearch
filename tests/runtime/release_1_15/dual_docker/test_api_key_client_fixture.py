"""Deterministic client security negatives; no Docker or external network."""
import base64
import unittest
from unittest.mock import patch
from api_key_client_fixture import Client, Unavailable

KEY = base64.b64encode(b"A" * 32).decode("ascii")
GOOD = b'{"fixture":true,"status":"unavailable"}'


class Socket:
    def settimeout(self, value):
        if value <= 0:
            raise TimeoutError()


class Response:
    status = 200
    length = 0

    def __init__(self, data=GOOD, status=200, content_type="application/json", declared=None):
        self.data, self.status, self.content_type, self.declared = data, status, content_type, declared
        self.length = 0

    def getheader(self, name, default=None):
        return {"Content-Type": self.content_type, "Content-Length": self.declared}.get(name, default)

    def close(self):
        self.closed = True

    def read1(self, count):
        data, self.data = self.data[:count], self.data[count:]
        return data


class Connection:
    def __init__(self, response):
        self.response, self.sock, self.requests, self.closed = response, Socket(), [], False

    def connect(self):
        pass

    def request(self, *args, **kwargs):
        self.requests.append((args, kwargs))

    def getresponse(self):
        return self.response

    def close(self):
        self.closed = True


class ClientTests(unittest.TestCase):
    def refresh(self, response, client=None):
        client = client or Client("https://research", KEY)
        connection = Connection(response)
        with patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=connection):
            result = client.refresh()
        self.assertTrue(connection.closed)
        return result, client, connection

    def test_only_https_origins(self):
        for url in ["http://research", "https://a@research", "https://research?key=x", "https://research/#x", "https://research/path", "https://research:bad", "https://research "]:
            with self.subTest(url=url), self.assertRaises(Unavailable):
                Client(url, KEY)

    def test_key_format_strict(self):
        for key in [None, "", "A" * 44, KEY + "\n", KEY[:-1], 123, base64.b64encode(b"A" * 33).decode()]:
            with self.subTest(key=type(key)), self.assertRaises(Unavailable):
                Client("https://research", key)

    def test_tls_verification_default(self):
        import ssl
        client = Client("https://research", KEY)
        self.assertTrue(client.context.check_hostname)
        self.assertEqual(client.context.verify_mode, ssl.CERT_REQUIRED)

    def test_key_only_in_server_header(self):
        result, client, connection = self.refresh(Response())
        self.assertEqual(result, {"fixture": True, "status": "unavailable"})
        self.assertTrue(client.connected)
        args, kwargs = connection.requests[0]
        self.assertEqual(args, ("GET", "/fixture/status"))
        self.assertEqual(kwargs, {"headers": {"X-API-Key": KEY}})
        self.assertNotIn(KEY, str(result))

    def test_redirects_and_errors_never_followed(self):
        for status in [301, 302, 307, 308, 401, 403, 429, 500, 503]:
            with self.subTest(status=status):
                connection = Connection(Response(status=status))
                with patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=connection):
                    with self.assertRaisesRegex(Unavailable, "^Request unavailable.$"):
                        Client("https://research", KEY).refresh()
                self.assertEqual(len(connection.requests), 1)
                self.assertTrue(connection.closed)

    def test_invalid_complete_response_is_atomic(self):
        client = Client("https://research", KEY)
        self.refresh(Response(), client)
        old = client.last_known
        for data in [b'\xff', b'{}', b'{"fixture":1,"status":"unavailable"}', b'{"fixture":true,"fixture":false,"status":"unavailable"}', b'{"fixture":true,"status":"unavailable","raw":1}', b'{"fixture":true', GOOD + b'{}']:
            with self.subTest(data=data), patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=Connection(Response(data))):
                with self.assertRaises(Unavailable):
                    client.refresh()
                self.assertIs(client.last_known, old)
                self.assertFalse(client.connected)

    def test_stream_and_declared_response_bounds(self):
        for response in [Response(b"x" * 4097), Response(declared="4097"), Response(content_type="text/html")]:
            with patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=Connection(response)), self.assertRaises(Unavailable):
                Client("https://research", KEY).refresh()

    def test_truncated_length_rejected(self):
        response = Response()
        response.length = 5
        with patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=Connection(response)), self.assertRaises(Unavailable):
            Client("https://research", KEY).refresh()

    def test_connect_budget_exhaustion_prevents_key_write(self):
        connection = Connection(Response())
        with patch("api_key_client_fixture.time.monotonic", side_effect=[0, 0, 5]), patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=connection), self.assertRaises(Unavailable):
            Client("https://research", KEY).refresh()
        self.assertEqual(connection.requests, [])
        self.assertTrue(connection.closed)

    def test_elapsed_budget_not_reset_between_phases(self):
        connection = Connection(Response())
        with patch("api_key_client_fixture.time.monotonic", side_effect=[0, 0, 1, 3, 5]), patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=connection), self.assertRaises(Unavailable):
            Client("https://research", KEY).refresh()
        self.assertEqual(len(connection.requests), 1)
        self.assertTrue(connection.closed)

    def test_timeout_preserves_state_then_normal_request_recovers(self):
        client = Client("https://research", KEY)
        self.refresh(Response(), client)
        old = client.last_known
        connection = Connection(Response())
        with patch.object(connection, "connect", side_effect=TimeoutError()), patch("api_key_client_fixture.http.client.HTTPSConnection", return_value=connection), self.assertRaises(Unavailable):
            client.refresh()
        self.assertIs(client.last_known, old)
        self.assertFalse(client.connected)
        self.assertTrue(connection.closed)
        self.refresh(Response(), client)
        self.assertTrue(client.connected)

    def test_watchdog_bounds_header_read_and_is_cancelled(self):
        from unittest.mock import MagicMock
        timer = MagicMock()
        with patch("api_key_client_fixture.threading.Timer", return_value=timer) as factory:
            self.refresh(Response())
        self.assertLessEqual(factory.call_args.args[0], 4)
        timer.start.assert_called_once()
        timer.cancel.assert_called_once()

    def test_network_errors_sanitized(self):
        with patch("api_key_client_fixture.http.client.HTTPSConnection", side_effect=OSError("sensitive dummy detail")), self.assertRaises(Unavailable) as error:
            Client("https://research", KEY).refresh()
        self.assertEqual(str(error.exception), "Request unavailable.")
        self.assertIsNone(error.exception.__cause__)

    def test_limits_configuration_rejected(self):
        for kwargs in [{"timeout": 0}, {"timeout": 5}, {"limit": 0}, {"limit": 4097}]:
            with self.assertRaises(Unavailable):
                Client("https://research", KEY, **kwargs)


if __name__ == "__main__":
    unittest.main(verbosity=2)
