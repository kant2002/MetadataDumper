# MetadataDumper

Dump .NET metadata information into CSV files. Each metadata table saved to separate CSV file.

## Usage

Dumping metadata

```
dotnet run --project MetadataDumper\MetadataDumper.csproj "C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App\8.0.4\Microsoft.AspNetCore.Authentication.Abstractions.dll" Microsoft.AspNetCore.Authentication.Abstractions
```

# MetadataRewriter

Rewriting metadata based on CSV files

```
dotnet run --project MetadataDumper\MetadataRewriter.csproj some.dll output.dll FolderWithRenaming\
```

Format of files in folder.

- Type renaming `TypeRenames.csv` with columns `TokenHandle,NewName`
- Type renaming `MethodRenames.csv` with columns `TokenHandle,NewName`