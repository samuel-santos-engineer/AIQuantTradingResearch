#!/bin/sh
# No tracing, credentials, export contents or full environment in diagnostics.
set -eu
mode=${AIQ_RESEARCH_BOOTSTRAP_MODE:-serve}
case "$mode" in
  diagnose)
    # Correlation is not attestation: independently authenticated retrieval is required.
    nonce=${AIQ_WP11_ATTEMPT_NONCE:-}
    digest=${AIQ_WP11_CANDIDATE_DIGEST:-}
    case "$nonce" in *[!0-9a-f]*|'') exit 1 ;; esac
    [ "${#nonce}" -eq 32 ] || exit 1
    case "$digest" in sha256:*) ;; *) exit 1 ;; esac
    hex=${digest#sha256:}
    case "$hex" in *[!0-9a-f]*|'') exit 1 ;; esac
    [ "${#hex}" -eq 64 ] || exit 1
    uid=$(awk '/^Uid:/ {printf "[%s,%s,%s,%s]",$2,$3,$4,$5}' /proc/self/status)
    [ -n "$uid" ] || exit 1
    mounts=$(awk '$5 == "/" {for(i=7;i<=NF;i++) if($i=="-") {
      printf "\"rootOverlay\":%s,\"rootWritable\":%s", ($(i+1)=="overlay"?"true":"false"), ($6 ~ /(^|,)rw(,|$)/?"true":"false");exit}}' /proc/self/mountinfo)
    [ -n "$mounts" ] || exit 1
    # Captures only fixed booleans; no database/server/provider composition occurs.
    probe=$(dotnet /app/AIQuantTradingResearch.Worker.dll --research-storage-probe)
    case "$probe" in 'WP11_STORAGE_PROBE '*) probe=${probe#WP11_STORAGE_PROBE } ;; *) exit 1 ;; esac
    printf '{"version":1,"nonce":"%s","digest":"%s","observedUtc":"%s","entrypoint":"research-bootstrap/diagnose","uid":%s,%s,"probe":%s}\n' \
      "$nonce" "$digest" "$(date -u +%Y-%m-%dT%H:%M:%SZ)" "$uid" "$mounts" "$probe"
    exit 0
    ;;
  operator-stdin)
    # A private read-only export must already be securely delivered to this same container.
    # Its source/UTC/permissions/row bounds and stdin configuration are validated by C#.
    if [ -z "${AIQ_RESEARCH_IMPORT_FILE:-}" ] || [ ! -r "$AIQ_RESEARCH_IMPORT_FILE" ]; then
      printf 'REIMPORT_REQUIRED; private input unavailable\n' >&2
      exit 1
    fi
    if ! dotnet /app/AIQuantTradingResearch.Worker.dll --research-initialize; then
      printf 'REIMPORT_REQUIRED; initialization rejected\n' >&2
      exit 1
    fi
    unset AIQ_RESEARCH_IMPORT_FILE AIQ_RESEARCH_BOOTSTRAP_MODE
    ;;
  serve) ;;
  *) printf 'Research bootstrap unavailable\n' >&2; exit 1 ;;
esac
# Initializer has exited and released ownership. No child server or second writer.
# Missing/lost/corrupt state is rejected by the unchanged endpoint, never auto-fetched.
exec dotnet /app/AIQuantTradingResearch.Worker.dll --research-snapshot-api
