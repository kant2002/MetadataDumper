Param(
    [Parameter(Mandatory=$false)]
    [string] $OutFolder = "out",
    [string] $Version = "8.0.4"
)

if ($false)
{
    .\process.ps1 -PackName Microsoft.AspNetCore.App -Version $Version -OutFolder $OutFolder
    .\process.ps1 -PackName Microsoft.NETCore.App -Version $Version -OutFolder $OutFolder
    .\process.ps1 -PackName Microsoft.WindowsDesktop.App -Version $Version -OutFolder $OutFolder
}

$data = @()
$fileData = $(ConvertFrom-Csv $(Get-Content "$OutFolder\Microsoft.AspNetCore.App_summary.csv"))
$data = $($data ; $fileData)
$fileData = $(ConvertFrom-Csv $(Get-Content "$OutFolder\Microsoft.NETCore.App_summary.csv"))
$data = $($data ; $fileData)

$summary = $data | Group-Object Word |ForEach-Object {
  [PsCustomObject]@{
    Word  = $_.Name
    Count = $_.Group | Measure-Object Count -Sum | ForEach-Object Sum
  }
} | Where-Object -FilterScript { $_.Word.Length -gt 1 }

$summary | ConvertTo-CSV | Out-File "$OutFolder\summary_web.csv"

$fileData = $(ConvertFrom-Csv $(Get-Content "$OutFolder\Microsoft.WindowsDesktop.App_summary.csv"))
$data = $($data ; $fileData)

$summary = $data | Group-Object Word |ForEach-Object {
  [PsCustomObject]@{
    Word  = $_.Name
    Count = $_.Group | Measure-Object Count -Sum | ForEach-Object Sum
  }
} | Where-Object -FilterScript { $_.Word.Length -gt 1 }

$summary | ConvertTo-CSV | Out-File "$OutFolder\summary.csv"
