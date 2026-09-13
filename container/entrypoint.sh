#!/bin/sh
set -eu

worker_pid=''
streamlit_pid=''

stop_children() {
    trap - TERM INT EXIT
    if [ -n "$worker_pid" ] && kill -0 "$worker_pid" 2>/dev/null; then
        kill -TERM "$worker_pid" 2>/dev/null || true
    fi
    if [ -n "$streamlit_pid" ] && kill -0 "$streamlit_pid" 2>/dev/null; then
        kill -TERM "$streamlit_pid" 2>/dev/null || true
    fi
    wait "$worker_pid" 2>/dev/null || true
    wait "$streamlit_pid" 2>/dev/null || true
}

fail() {
    printf '%s\n' "aiq-entrypoint: $*" >&2
    exit 64
}

require_value() {
    variable_name="$1"
    eval "variable_value=\${$variable_name-}"
    [ -n "$variable_value" ] || fail "required environment variable $variable_name is not set"
}

configured_persistence_parent() {
    dirname "$Persistence__DatabasePath"
}

validate_persistence_parent() {
    persistence_parent="$1"
    case "$persistence_parent" in
        /home/*|/runtime|/runtime/*)
            ;;
        *)
            fail "configured persistence parent is outside the approved runtime paths"
            ;;
    esac
}

prepare_persistence_parent_as_root() {
    persistence_parent="$1"
    mkdir -p "$persistence_parent"
    chown 1000:1000 "$persistence_parent"
    chmod 0750 "$persistence_parent"
}

require_writable_persistence_parent() {
    persistence_parent="$1"
    [ -d "$persistence_parent" ] || fail "configured persistence parent does not exist"
    [ -w "$persistence_parent" ] || fail "configured persistence parent is not writable by aiq"
}

if [ "${Worker__Mode-}" = "PersistentSqliteQualification" ]; then
    require_value Persistence__DatabasePath
    require_value PersistentSqliteQualification__Phase
else
    require_value TwelveData__ApiKey
    require_value Dataset__Target
    require_value Dataset__From
    require_value Dataset__To
    require_value Worker__Replay__ReplayIdentity
    require_value Worker__Replay__Target
    require_value Worker__Replay__StartingTick
    require_value Worker__Replay__RequestedObservationCount
fi

persistence_parent="$(configured_persistence_parent)"
validate_persistence_parent "$persistence_parent"

if [ "$(id -u)" -eq 0 ]; then
    prepare_persistence_parent_as_root "$persistence_parent"
    exec gosu aiq "$0" "$@"
fi

require_writable_persistence_parent "$persistence_parent"

mkdir -p "$(dirname "$Visualization__HandoffPath")"

qualification_http_mode=false
if [ "${Worker__Mode-}" = "PersistentSqliteQualification" ] \
    && [ "${PersistentSqliteQualification__HttpEvidenceEnabled-false}" = "true" ]; then
    qualification_http_mode=true
fi

on_signal() {
    printf '%s\n' 'aiq-entrypoint: termination signal received; stopping required children' >&2
    stop_children
    exit 0
}

trap on_signal TERM INT

printf '%s\n' 'aiq-entrypoint: starting Worker replay process' >&2
dotnet /app/worker/AIQuantTradingResearch.Worker.dll &
worker_pid=$!

if [ "$qualification_http_mode" = true ]; then
    printf '%s\n' 'aiq-entrypoint: qualification HTTP evidence mode; Streamlit suppressed' >&2
    wait "$worker_pid" || worker_status=$?
    worker_status=${worker_status:-0}
    worker_pid=''
    printf '%s\n' "aiq-entrypoint: Worker exited with status $worker_status" >&2
    exit "$worker_status"
fi

printf '%s\n' 'aiq-entrypoint: starting Streamlit presentation process' >&2
streamlit run /app/python/presentation/realtime_financial_visualization.py \
    --server.address "$STREAMLIT_SERVER_ADDRESS" \
    --server.port "$STREAMLIT_SERVER_PORT" \
    --server.headless true &
streamlit_pid=$!

while :; do
    if [ -n "$worker_pid" ] && ! kill -0 "$worker_pid" 2>/dev/null; then
        wait "$worker_pid" || worker_status=$?
        worker_status=${worker_status:-0}
        printf '%s\n' "aiq-entrypoint: Worker exited with status $worker_status" >&2
        worker_pid=''
        if [ "$worker_status" -ne 0 ]; then
            stop_children
            exit "$worker_status"
        fi
        printf '%s\n' 'aiq-entrypoint: Worker completed successfully; retaining Streamlit presentation' >&2
    fi
    if ! kill -0 "$streamlit_pid" 2>/dev/null; then
        wait "$streamlit_pid" || streamlit_status=$?
        streamlit_status=${streamlit_status:-0}
        printf '%s\n' "aiq-entrypoint: Streamlit exited with status $streamlit_status" >&2
        stop_children
        exit "$streamlit_status"
    fi
    sleep 1
done
