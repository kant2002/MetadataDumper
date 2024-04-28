$SharedFolder = "C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App\8.0.4"
$OutFolder = "out"
$assemblies = @("Microsoft.AspNetCore.Authentication.Abstractions")
$assemblies = $(ls $SharedFolder -Filter "*.dll" | Select -ExpandProperty BaseName)
#$x | Select BaseName
foreach($assembly in $assemblies)
{
    dotnet run --project MetadataDumper "$SharedFolder\$assembly.dll" "$OutFolder\$assembly"
}
