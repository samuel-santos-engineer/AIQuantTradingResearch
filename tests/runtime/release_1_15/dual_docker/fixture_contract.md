# WP02 paired-image security fixture

UTF-8/LF. Fixture only; `PUBLIC_RUNTIME_VERIFIED=False`.

Research: non-root UID/GID12001, one dotnet principal process, TLS52731. UI: UID/GID12002, one Streamlit principal process, internal8501. No in-container supervisor/restart. Explicit host labels and loopback published test ports only. Root Dockerfile, actual applications and Azure settings unchanged.

Only GET /health/live is anonymous, returns generic ok. All other paths authenticate before status/body processing. GET /fixture/status returns only {"fixture":true,"status":"unavailable"}; POST /v1/research/snapshot accepts only empty JSON object and returns503 fixture unavailable. This is NOT a D4 implementation. Other authenticated paths404; unauthorized requests401 without echoed input. Request body4096, headers4096/16, concurrent connections8, request admission4, header/body deadlines4s; no promise of distributed/global abuse resistance.

Service keys are dummy32-byte canonical Base64 strings, independent from provider/HMAC. Active + one previous key, explicit UTC expiry at most300s from startup; monotonic cap, backward UTC expires previous. Expired previous never accepted by this process. Restart/config rollback cannot be proven safe by memory-only fixture state; WP05/WP07 must establish durable configuration/clock evidence before production adoption. No rotation endpoint, logs, values or browser material. Comparisons use fixed-size SHA256 digests/constant-time comparison; complete request timing is not constant.

UI accepts only HTTPS origins without userinfo/query/fragment; verifies chain/hostname; no redirects, proxies or plaintext fallback. Four-second total budget,4096-byte response cap, exact fixture JSON validation. Transport errors preserve prior complete fixture status and mark disconnected. Test self-signed CA/leaf expires after2h; its private key remains in disposable research-only mount, UI receives only public trust.pem. Never deploy fixture certificates/dummy keys.

WP03 owns data/writer recovery; WP05 owns production API/header/key lifecycle and unchanged D4/D5; WP07 verifies actual Azure TLS termination, proxy trust, ingress/auth and F1 resource/rotation/paired image behavior. Public Azure hostnames are not private networking. No registry/Azure/provider operations permitted by this fixture.
