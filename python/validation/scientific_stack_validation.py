"""Release 1.8 WP08 deterministic, offline scientific-stack validation.

This is validation evidence only. It is not production Python, a model
artifact, a data pipeline, or Release 1.9 behavior.
"""

from __future__ import annotations

from pathlib import Path

import numpy as np
import pandas as pd
import sklearn
from sklearn.linear_model import LinearRegression
from streamlit.testing.v1 import AppTest


ROOT = Path(__file__).resolve().parent
APP = ROOT / "streamlit_validation_app.py"


def validate_numpy() -> None:
    prices = np.array([100.0, 102.0, 101.0, 104.0])
    changes = np.diff(prices)
    np.testing.assert_allclose(changes, [2.0, -1.0, 3.0])
    np.testing.assert_allclose(prices / prices[0], [1.0, 1.02, 1.01, 1.04])
    print("NUMPY PASS: vectorized deltas and normalization")


def validate_pandas() -> None:
    frame = pd.DataFrame(
        {"symbol": ["AAA", "BBB", "AAA"], "value": [2, 5, 3]}
    )
    summary = (
        frame.assign(weighted=frame["value"] * 2)
        .groupby("symbol", sort=True, as_index=False)["weighted"]
        .sum()
    )
    assert list(summary.columns) == ["symbol", "weighted"]
    assert summary.to_dict("records") == [
        {"symbol": "AAA", "weighted": 10},
        {"symbol": "BBB", "weighted": 10},
    ]
    print("PANDAS PASS: deterministic derived-column aggregation")


def validate_scikit_learn() -> None:
    features = np.array([[0.0], [1.0], [2.0], [3.0]])
    targets = np.array([1.0, 3.0, 5.0, 7.0])
    estimator = LinearRegression()
    estimator.fit(features, targets)
    predictions = estimator.predict(np.array([[4.0], [5.0]]))
    np.testing.assert_allclose(predictions, [9.0, 11.0])
    assert predictions.shape == (2,)
    print(f"SCIKIT_LEARN PASS: LinearRegression fit/predict ({sklearn.__version__})")


def validate_streamlit() -> None:
    app = AppTest.from_file(str(APP)).run(timeout=10)
    assert not app.exception
    assert app.title[0].value == "WP08 Scientific Stack Validation"
    assert app.metric[0].label == "Fixed validation rows"
    assert app.metric[0].value == "3"
    print("STREAMLIT PASS: AppTest rendered deterministic title/metric")


def main() -> None:
    validate_numpy()
    validate_pandas()
    validate_scikit_learn()
    validate_streamlit()
    print("WP08 VALIDATION PASS: 4/4 deterministic offline use cases")


if __name__ == "__main__":
    main()
