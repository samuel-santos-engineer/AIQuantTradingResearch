#!/usr/bin/env sh
set -eu

script_dir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
repository_root=$(CDPATH= cd -- "$script_dir/.." && pwd)
solution_path=${1:-AIQuantTradingResearch.slnx}
configuration=${2:-Debug}

case "$solution_path" in
    /*) ;;
    *) solution_path="$repository_root/$solution_path" ;;
esac

if [ ! -f "$solution_path" ]; then
    printf '%s\n' "Solution file was not found: $solution_path" >&2
    exit 1
fi

printf '%s\n' "Building AIQuantTradingResearch solution ($configuration)..."
dotnet build "$solution_path" --configuration "$configuration" --no-restore --nologo
