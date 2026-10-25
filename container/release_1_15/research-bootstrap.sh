#!/bin/sh
# No tracing, credentials, export contents or full environment in diagnostics.
set -eu
mode=${AIQ_RESEARCH_BOOTSTRAP_MODE:-serve}
case "$mode" in
  diagnose)
    printf 'WP11_BOUNDED_DIAGNOSTIC\n'
    # Print only numeric UID fields and selected mount metadata; never mount sources/options after fs type.
    awk '/^Uid:/ {print "UID_FIELDS",$2,$3,$4,$5}' /proc/self/status
    awk '$5 == "/" || $5 == "/tmp" || $5 == "/tmp/aiq-research" {
      for(i=7;i<=NF;i++) if($i=="-") {print "MOUNT",$3,"rootIsSlash="($4=="/"),$5,$6,$(i+1);break}
    }' /proc/self/mountinfo
    for directory in /tmp /tmp/aiq-research; do
      if [ -L "$directory" ]; then printf 'PATH_SYMLINK\n';
      elif [ -d "$directory" ]; then stat -c 'PATH_MODE %u %g %a' "$directory";
      else printf 'PATH_ABSENT\n'; fi
    done
    for limit in memory.max cpu.max pids.max; do
      if [ -r "/sys/fs/cgroup/$limit" ]; then
        printf 'CGROUP_LIMIT %s ' "$limit"
        awk 'NR==1 {print;exit}' "/sys/fs/cgroup/$limit"
      else printf 'CGROUP_LIMIT_UNAVAILABLE\n'; fi
    done
    # Exact opt-in allows only the bounded private write probe; never SQLite initialization.
    dotnet /app/AIQuantTradingResearch.Worker.dll --research-storage-probe
    printf 'NO_IMPORT_NO_DATABASE_NO_SERVER; PUBLIC_RUNTIME_VERIFIED=False\n'
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
