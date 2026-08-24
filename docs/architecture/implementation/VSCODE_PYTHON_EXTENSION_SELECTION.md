# VS Code Python Extension Selection

## Decision

Select the official Microsoft Visual Studio Code Python extension:

- Identifier: `ms-python.python`
- Publisher: Microsoft
- Scope: normal VS Code user-extension scope
- Version policy: use a current stable Microsoft release compatible with the installed VS Code; record the installed version as execution evidence, but do not create an unnecessary permanent user-global pin.

The extension supplies Python editing and development integration, including
interpreter discovery/selection, testing, debugging, linting, formatting, and
environment access. It connects VS Code to Python; it does not include or own
the Python runtime. The machine-wide PSF CPython 3.13.15 installation remains
the runtime authority, and a future project-local venv becomes the preferred
project interpreter after WP05 creates it.

## Alternatives and trade-offs

A terminal-only workflow, manually managed interpreter invocation, other IDE
tooling, and additional Python extension packs were considered. Terminal-only
operation would provide less integrated discovery and verification; other IDEs
or extension packs add unrelated tooling and scope. The Microsoft extension
is selected because it is the official VS Code Python integration and is
appropriate for the governed PowerShell/VS Code workflow.

Accepted trade-offs are an additional developer-tool dependency, extension
lifecycle/version drift, Microsoft-specific VS Code integration, and possible
behavior changes across releases. The extension is not a production,
application, Python-package, or .NET dependency.

## Boundaries, security, and ownership

Installation is limited to the existing user's VS Code extension scope.
Only dependencies automatically introduced by the official extension install
are accepted and must be recorded; Jupyter, project packages, and unrelated
extensions are not selected. Marketplace provenance and publisher identity
must be read back after installation. Marketplace telemetry/privacy behavior
remains subject to Microsoft's documentation and the user's VS Code privacy
settings.

No user-specific interpreter path, extension path, or machine-specific VS
Code setting may be committed. Repository policy remains portable: machine
Python bootstraps the workflow before the project venv exists, and the future
project-local venv is preferred afterward.

## Validation and reconsideration

Validate the official identifier/publisher, exact installed version, automatic
dependencies, VS Code discovery of Python 3.13.15, and unchanged global
runtime/package boundaries. Reconsider if Microsoft discontinues support,
the extension becomes incompatible with the governed VS Code/Python line,
security/privacy requirements change, or a portable workflow no longer needs
the extension.

## Release boundary and references

This is a Release 1.8 development-tool selection that unblocks WP04. It does
not create a project venv, install Python libraries, implement UI/ML/.NET
integration, or begin Release 1.9.

- [VS Code Python extension in the Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-python.python)
- [Python in Visual Studio Code](https://code.visualstudio.com/docs/languages/python)
- [Python VS Code tutorial](https://code.visualstudio.com/docs/python/python-tutorial)
