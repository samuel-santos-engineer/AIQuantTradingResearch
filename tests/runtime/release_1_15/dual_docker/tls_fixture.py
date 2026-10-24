"""Create short-lived offline TEST certificates through the owned research image."""
from pathlib import Path
import subprocess


def create(image, output, label):
    output = Path(output).resolve()
    output.mkdir(parents=True, exist_ok=True)
    subprocess.run(["docker", "run", "--rm", "--network", "none", "--cap-drop=ALL",
                    "--security-opt", "no-new-privileges", "--memory=128m", "--pids-limit=32",
                    "--label", "aiq.wp02=" + label,
                    "--mount", "type=bind,source=" + str(output) + ",target=/certs",
                    image, "--make-cert", "/certs"], check=True, timeout=45,
                   stdout=subprocess.DEVNULL)
    return output / "trust.pem"
