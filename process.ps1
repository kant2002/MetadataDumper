Param(
    [Parameter(Mandatory=$false)]
    [string] $OutFolder = "out",
    [ValidateSet("Microsoft.AspNetCore.App", "Microsoft.AspNetCore.All", "Microsoft.NETCore.App", "Microsoft.WindowsDesktop.App")]
    [string] $PackName = "Microsoft.AspNetCore.App",
    [string] $Version = "8.0.4",
    [switch] $DoNotExtractStrings
)

$SharedFolder = "C:\Program Files\dotnet\shared\$PackName\$Version"
#$OutFolder = "out"
$assemblies = @("Microsoft.AspNetCore.Authentication.Abstractions")
$assemblies = $(ls $SharedFolder -Filter "*.dll" | Select -ExpandProperty BaseName)
$data = @()
$ignoredDll = @(
    "aspnetcorev2_inprocess",
    "clretwrc",
    "clrgc",
    "clrjit",
    "coreclr",
    "hostpolicy",
    "Microsoft.DiaSymReader.Native.amd64",
    "mscordaccore",
    "mscordaccore_amd64_amd64_8.0.424.16909",
    "mscordbi",
    "mscorlib",
    "mscorrc",
    "msquic",
    "D3DCompiler_47_cor3",
    "PenImc_cor3",
    "vcruntime140_cor3",
    "wpfgfx_cor3",
    "PresentationNative_cor3",
    "System.IO.Compression.Native"
)
foreach($assembly in $assemblies)
{
    if ($ignoredDll.Contains($assembly)) 
    {
        continue
    }

    if (-not $DoNotExtractStrings)
    {
        dotnet run --project MetadataDumper "$SharedFolder\$assembly.dll" "$OutFolder\$assembly"
    }

    $fileData = $(ConvertFrom-Csv $(Get-Content "$OutFolder\$assembly\auxillary\MetadataWords.csv"))
    $data = $($data ; $fileData)
}

$summary = $data | Group-Object Word |ForEach-Object {
  [PsCustomObject]@{
    Word  = $_.Name
    Count = $_.Group | Measure-Object Count -Sum | ForEach-Object Sum
  }
}

$summary | ConvertTo-CSV | Out-File "$OutFolder\${PackName}_summary.csv"