[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Mandatory)] [string] $ResourceGroup
)

$ErrorActionPreference = 'Stop'
Write-Host '=== VERIFY OWNED WP03 RESOURCE GROUP ==='
$group = & az group show --name $ResourceGroup --query '{name:name,location:location,tags:tags}' --output json | ConvertFrom-Json
Write-Host "WP03_CLEANUP_PREDELETE_READ_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Resource group does not exist or cannot be read.' }
if ($group.tags.wp -ne 'WP03' -or $group.tags.initiative -ne 'Release-1.12' -or $group.tags.recurringCost -ne '0') { throw 'Refusing to delete a resource group without the exact WP03 ownership tags.' }

if ($PSCmdlet.ShouldProcess($ResourceGroup, 'Delete verified WP03 resource group')) {
    & az group delete --name $ResourceGroup --yes --no-wait
    Write-Host "WP03_CLEANUP_DELETE_REQUEST_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Resource-group deletion request failed.' }
}
else {
    Write-Host 'WP03_CLEANUP_DELETE_REQUEST=WHATIF'
}
