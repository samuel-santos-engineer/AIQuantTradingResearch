"""Fake presentation observations; never provider or research data."""
from datetime import datetime, timedelta, timezone


def snapshot(instrument="BTC/USD", version=1, generation="fixture-generation-a", day="2026-10-22", start=0, gap=None):
    origin = datetime(2026, 10, 20, tzinfo=timezone.utc)
    rows = []
    for index in range(start, start + 1000):
        if index == gap:
            continue
        close = 100 + index % 17 if instrument == "BTC/USD" else 20 + index % 7
        rows.append({"time": (origin + timedelta(minutes=index)).isoformat().replace("+00:00", "Z"),
                     "open": close, "high": close + 2, "low": close - 2, "close": close + 1, "volume": 10})
    return {"instrument": instrument, "generation": generation, "version": version, "displayDate": day, "rows": rows}
