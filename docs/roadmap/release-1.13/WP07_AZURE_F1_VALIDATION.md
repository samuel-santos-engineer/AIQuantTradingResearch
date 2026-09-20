# Release 1.13 WP07 Azure F1 validation record

Selected execution model: **GPT-5.6 Terra**.

## Scope

WP07 validates the existing Linux F1, public GHCR, persistent `/home`, custom-container topology. It introduces no paid resource, service, schema change, provider redesign, or public provider client.

## Runtime correction

The entrypoint creates `/home/aiq-market-cache` as root, assigns it to the non-root `aiq` account, and validates it after privilege drop. This preserves the accepted Worker-owned atomic cache and its bounded retention; it does not create a Python cache.

## Validation evidence

The focused container validator proves the published Worker DLL path, `dotnet` availability, and entrypoint shell syntax. Repository validation additionally covers the bounded Worker stdio protocol, controlled malformed response, cache lock/retention behavior, and presentation no-bypass rules.

## Live boundary

The existing West Central US App Service remains Linux F1/Free, HTTPS-only, and running. Live Vike-success smoke requires an already governed server-side `Vike__ApiKey`; its value is never read or recorded. No deployment is performed by this document.
