Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Stop'

$allowedPaths = @(
    'Dockerfile',
    'container/entrypoint.sh',
    'src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs',
    'src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs',
    'src/AIQuantTradingResearch.Worker/Program.cs',
    'tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs',
    'eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1',
    'eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1',
    'src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqlitePersistenceDiagnostics.cs',
    'src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs',
    'tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceDiagnosticsTests.cs'
)

Write-Host '=== BUILD ==='
dotnet build .\src\AIQuantTradingResearch.Worker\AIQuantTradingResearch.Worker.csproj --no-restore
Write-Host "WP04_BUILD_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

$testProjects = @(
    'tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj',
    'tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj',
    'tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj',
    'tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj'
)

foreach ($project in $testProjects) {
    Write-Host "=== TEST $project ==="
    dotnet test $project --no-restore --no-build --logger 'console;verbosity=minimal'
    Write-Host "WP04_TEST_EXIT_CODE[$project]=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}

Write-Host '=== TARGETED INFRASTRUCTURE TESTS ==='
dotnet test .\tests\AIQuantTradingResearch.Infrastructure.Tests\AIQuantTradingResearch.Infrastructure.Tests.csproj `
    --no-restore --no-build `
    --filter 'FullyQualifiedName~SqlitePersistenceTests|FullyQualifiedName~SqlitePersistenceDiagnosticsTests|FullyQualifiedName~ExperimentDiscoveryTests' `
    --logger 'console;verbosity=minimal'
Write-Host "WP04_TARGETED_TESTS_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host '=== POWERSHELL PARSE ==='
$parseFailures = @()
foreach ($path in @(
    'eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1',
    'eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1'
)) {
    $tokens = $null
    $errors = $null
    [void][System.Management.Automation.Language.Parser]::ParseFile(
        (Resolve-Path -LiteralPath $path),
        [ref]$tokens,
        [ref]$errors)
    Write-Host "WP04_POWERSHELL_PARSE_ERROR_COUNT[$path]=$($errors.Count)"
    $parseFailures += $errors
}
if ($parseFailures.Count -ne 0) { exit 1 }
Write-Host 'WP04_POWERSHELL_VALIDATION_EXIT_CODE=0'

Write-Host '=== ENTRYPOINT SHELL / DOCKERFILE STATIC CONTRACT ==='
bash -n .\container\entrypoint.sh
Write-Host "WP04_ENTRYPOINT_SHELL_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

$dockerfile = Get-Content -Raw -LiteralPath '.\Dockerfile'
$entrypoint = Get-Content -Raw -LiteralPath '.\container\entrypoint.sh'
Write-Host "WP04_DOCKERFILE_GOSU_PRESENT=$($dockerfile -match 'apt-get install .*gosu')"
Write-Host "WP04_DOCKERFILE_ROOT_STARTUP_PRESENT=$($dockerfile -match '(?m)^USER root$')"
Write-Host "WP04_ENTRYPOINT_PRIVILEGE_DROP_PRESENT=$($entrypoint -match 'exec gosu aiq')"
Write-Host "WP04_ENTRYPOINT_SQLITE_COMMAND_ABSENT=$(-not ($entrypoint -match '(?i)(sqlite3|pragma|insert into|select .* from)'))"

Write-Host '=== DIFF CHECK ==='
git diff --check
Write-Host "WP04_DIFF_CHECK_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host '=== ELEVEN-PATH AUDIT ==='
$modifiedPaths = @(git diff --name-only)
$untrackedPaths = @(git ls-files --others --exclude-standard)
$candidatePaths = @($modifiedPaths + $untrackedPaths | Sort-Object -Unique)
$unexpectedPaths = @($candidatePaths | Where-Object { $_ -notin $allowedPaths })
$missingAllowedPaths = @($allowedPaths | Where-Object { $_ -notin $candidatePaths })
Write-Host "WP04_ALLOWLIST_CANDIDATE_COUNT=$($candidatePaths.Count)"
Write-Host "WP04_ALLOWLIST_UNEXPECTED_COUNT=$($unexpectedPaths.Count)"
Write-Host "WP04_ALLOWLIST_MISSING_COUNT=$($missingAllowedPaths.Count)"
$unexpectedPaths | ForEach-Object { Write-Host "WP04_ALLOWLIST_UNEXPECTED=$_"}
$missingAllowedPaths | ForEach-Object { Write-Host "WP04_ALLOWLIST_MISSING=$_"}
Write-Host "WP04_STAGED_PATH_COUNT=$(@(git diff --cached --name-only).Count)"
if ($missingAllowedPaths.Count -ne 0) { exit 1 }
Write-Host 'WP04_ALLOWLIST_AUDIT_EXIT_CODE=0'

Write-Host '=== GITLEAKS: ELEVEN PATHS ONLY ==='
$scanRoot = Join-Path ([System.IO.Path]::GetTempPath()) ('aiq-wp04-gitleaks-' + [guid]::NewGuid().ToString('N'))
try {
    foreach ($path in $allowedPaths) {
        $source = Join-Path (Get-Location) $path
        $destination = Join-Path $scanRoot $path
        New-Item -ItemType Directory -Path (Split-Path -Parent $destination) -Force | Out-Null
        Copy-Item -LiteralPath $source -Destination $destination -Force
    }
    gitleaks detect --no-git --source $scanRoot --redact --exit-code 1
    Write-Host "WP04_GITLEAKS_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}
finally {
    Remove-Item -LiteralPath $scanRoot -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_GITLEAKS_TEMP_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $scanRoot)"
}

Write-Host 'WP04_FULL_LOCAL_VALIDATION_EXIT_CODE=0'