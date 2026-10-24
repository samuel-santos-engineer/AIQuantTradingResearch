"""Independent CPython HMAC/integer-rounding oracle and real-HTTPS integration driver."""
import hashlib
import hmac
import json
import pathlib
import sys


def reference(day):
    message = ("aiq-r115-transform-v1\n" + day + "\n11111111-1111-4111-8111-111111111111\n").encode("ascii")
    digest = hmac.digest(bytes(range(32)), message, "sha256")
    factor = 900000000000 + int.from_bytes(digest, "big") * 200000000001 // 2**256
    return message, digest, factor


def rounded(n, d):
    q, r = divmod(abs(n), d)
    if r * 2 > d or r * 2 == d and q % 2:
        q += 1
    return -q if n < 0 else q


def fixed(n):
    return ("-" if n < 0 else "") + str(abs(n) // 10**12) + "." + str(abs(n) % 10**12).zfill(12)


if __name__ == "__main__":
    root, url, ca, key = sys.argv[1:]
    sys.path.insert(0, root)
    from python.presentation.research_snapshot_client import ResearchSnapshotClient
    from python.presentation.research_snapshot_client import SnapshotUnavailable
    import socket
    import ssl
    import urllib.parse
    try:
        ResearchSnapshotClient(url, key).fetch("BTC/USD")
    except SnapshotUnavailable:
        pass
    else:
        raise AssertionError("Untrusted certificate accepted")
    address = urllib.parse.urlsplit(url)
    try:
        with socket.create_connection((address.hostname, address.port), timeout=3) as stream:
            with ssl.create_default_context(cafile=ca).wrap_socket(stream, server_hostname="wrong.offline.invalid"):
                raise AssertionError("Wrong TLS hostname accepted")
    except ssl.SSLCertVerificationError:
        pass
    client = ResearchSnapshotClient(url, key, ca_file=ca)
    for instrument in ("BTC/USD", "ETH/USD"):
        full = client.fetch(instrument)
        assert full["type"] == "full" and len(full["candles"]) == 401
        message, digest, factor = reference(full["identity"]["displayDate"])
        expected = fixed(rounded(100 * 10**12 * factor, 10**12))
        assert full["candles"][0]["open"] == expected
        assert full["analytics"][-1]["sma20"] == expected
        assert full["status"]["research"] == "Ready"
        assert full["classifications"][-1]["validity"] == "Valid"
        unchanged = client.fetch(instrument)
        assert unchanged["type"] == "unchanged" and unchanged["candles"] == full["candles"]
        # Independently construct the constant-series public wire, including seeding and events.
        # Only random lineage labels come from the response; expected numeric values are not copied.
        import datetime as dt
        import http.client
        segment = full["candles"][0]["segment"]
        transform = lambda price: fixed(rounded(price * 10**12 * factor, 10**12))
        minute = lambda index: (dt.datetime(2026, 10, 1, tzinfo=dt.timezone.utc) + dt.timedelta(minutes=index)).strftime("%Y-%m-%dT%H:%M:00Z")
        candles, analytics, classifications = [], [], []
        for index in range(1, 402):
            t = minute(index - 1)
            candles.append(dict(time=t, segment=segment, segmentIndex=str(index), open=transform(100), high=transform(101),
                                low=transform(99), close=transform(100), volume="1.000000000000"))
            analytics.append(dict(time=t, sma20=transform(100) if index >= 20 else None, ema20=transform(100) if index >= 20 else None,
                                  bollingerLower=transform(100) if index >= 20 else None, bollingerMiddle=transform(100) if index >= 20 else None,
                                  bollingerUpper=transform(100) if index >= 20 else None, rsi14="50.000000000000" if index >= 15 else None,
                                  macd="0.000000000000" if index >= 26 else None, macdSignal="0.000000000000" if index >= 34 else None,
                                  macdHistogram="0.000000000000" if index >= 34 else None, atr14=transform(2) if index >= 14 else None,
                                  normalizedAtr14="0.020000000000" if index >= 14 else None, bollingerWidth="0.000000000000" if index >= 20 else None,
                                  emaSlope5="0.000000000000" if index >= 25 else None))
            classifications.append(dict(time=t, segment=segment, validity="Valid" if index == 401 else "Unavailable",
                                        trendScore=0 if index == 401 else None, trend="Neutral" if index == 401 else None,
                                        regime="Undetermined" if index == 401 else None))
        observed = "2026-10-23T12:01:15.000000Z"
        expected_wire = dict(schemaVersion="1.0", type="full", instrument=instrument, requestEpoch="99", observedAt=observed,
                             identity=dict(full["identity"]), contract=dict(researchAlgorithm="aiq-r115-research-v1", transformAlgorithm="aiq-r115-transform-v1", intervalSeconds=60),
                             status=dict(observedAt=observed, research="Ready", freshness="Stale", integrity="Protected", classificationValidity="Valid",
                                         lastCompletedCandleAt=minute(400), lastSuccessfulProviderPollAt=observed, lastCommitAt=observed, researchAsOfCandleAt=minute(400)),
                             candles=candles, analytics=analytics, classifications=classifications,
                             events=[dict(time=minute(0), segment=segment, type="SegmentStart", previousTrend=None, trend=None, regime=None),
                                     dict(time=minute(400), segment=segment, type="TrendTransition", previousTrend=None, trend="Neutral", regime="Undetermined")],
                             researchUnavailableIntervals=[dict(start=minute(0), end=minute(400))], marketDataGapIntervals=[])
        connection = http.client.HTTPSConnection(address.hostname, address.port, timeout=5, context=ssl.create_default_context(cafile=ca))
        try:
            request = dict(schemaVersion="1.0", instrument=instrument, requestEpoch="99", knownIdentity=None)
            connection.request("POST", "/v1/research/snapshot", json.dumps(request).encode(), {"X-API-Key": key, "Content-Type": "application/json"})
            response = connection.getresponse()
            assert response.status == 200
            actual_bytes = response.read(4194305)
        finally:
            connection.close()
        expected_bytes = json.dumps(expected_wire, separators=(",", ":")).encode("utf-8")
        assert actual_bytes == expected_bytes, "Independent complete public byte comparison failed"
        print(instrument + " full401/unchanged/independent-D5 PASS " + digest.hex())
        print(instrument + " BYTE_MATCH " + str(len(actual_bytes)) + " sha256=" + hashlib.sha256(actual_bytes).hexdigest())
