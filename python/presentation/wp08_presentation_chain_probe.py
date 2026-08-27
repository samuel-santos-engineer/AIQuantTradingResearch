"""WP08 one-shot evidence probe for the existing presentation chain."""
from __future__ import annotations

import argparse
import json
import sys
from pathlib import Path

from realtime_financial_visualization import (
    project_visualization_frame,
    project_wp07_presentation_sections,
)
from visualization_read_model import parse_envelope


def main() -> int:
    parser = argparse.ArgumentParser(add_help=False)
    parser.add_argument("--handoff", required=True)
    try:
        args = parser.parse_args()
        handoff = Path(args.handoff)
        if not handoff.is_absolute() or not handoff.is_file():
            raise ValueError("--handoff must name an existing absolute file")
    except (SystemExit, ValueError) as exc:
        print(str(exc), file=sys.stderr)
        return 2

    try:
        envelope = parse_envelope(handoff.read_text(encoding="utf-8"))
        frame = project_visualization_frame(envelope)
        sections = project_wp07_presentation_sections(frame)
        raw = envelope.raw
        revision = raw["revision"]
        latest = frame.latest
        result = {
            "contract": "aiq-wp08-presentation-chain-probe-v1",
            "source": {
                "contractVersion": raw["contractVersion"],
                "revisionKind": revision["kind"],
                "revisionValue": revision["value"],
                "revisionIdentity": revision["identity"],
                "sourceMode": raw["sourceMode"],
                "state": raw["state"],
                "snapshotIdentity": raw.get("snapshotIdentity"),
                "datasetVersion": raw.get("datasetVersion"),
            },
            "frame": {
                "revisionKind": frame.revision_kind,
                "revisionValue": frame.revision_value,
                "revisionIdentity": frame.revision_identity,
                "state": frame.state,
                "pointCount": len(frame.points),
                "observationCount": frame.observation_count,
                "windowCount": frame.window_count,
                "windowCapacity": frame.window_capacity,
                "latestSourceTime": None if latest is None else latest.source_time,
                "latestPrice": None if latest is None else format(latest.price, "f"),
                "idempotencyStatus": frame.idempotency_status,
                "dataQualityStatus": frame.data_quality_status,
            },
            "sections": [
                {"label": label, "rows": [list(row) for row in rows]}
                for label, rows in sections
            ],
        }
        print(json.dumps(result, ensure_ascii=False, separators=(",", ":")))
        return 0
    except Exception as exc:
        print(f"presentation-chain probe failed: {exc}", file=sys.stderr)
        return 3


if __name__ == "__main__":
    raise SystemExit(main())
