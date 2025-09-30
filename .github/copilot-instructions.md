# Copilot Instructions for MetadataDumper

## Project Architecture

This is a .NET metadata analysis toolkit with three main components:

1. **MetadataDumper** - Extracts .NET assembly metadata into CSV files using dnlib
2. **MetadataRewriter** - Modifies assembly metadata based on CSV rename rules  
3. **MetadataDumper.Tests** - Unit tests for the tokenizer component

## Key Dependencies & Patterns

- **dnlib 4.5.0**: Core library for .NET metadata manipulation - all metadata operations use `dnlib.DotNet` types
- **Microsoft.Data.Sqlite**: Used for English word analysis against `en.sqlite3` database
- **CSV-first approach**: All metadata tables are exported as individual CSV files with specific column schemas

## Critical Workflows

### Running MetadataDumper
```bash
dotnet run --project MetadataDumper\MetadataDumper.csproj "path\to\assembly.dll" [output_directory]
```

### Running MetadataRewriter  
```bash
dotnet run --project MetadataRewriter\MetadataRewriter.csproj input.dll output.dll folder_with_csv_rules\
```

### Bulk Processing .NET Shared Frameworks
```powershell
.\process.ps1 -PackName "Microsoft.AspNetCore.App" -Version "8.0.4" -OutFolder "out"
.\summary.ps1 -Version "8.0.4" -OutFolder "out"
```

## Code Patterns

### Metadata Extraction Pattern
- `MetadataExporter` creates one CSV per metadata table (TypeDef.csv, Method.csv, etc.)
- Each table method follows pattern: `Print{TableName}Table(TextWriter writer)`
- String collection happens via `AddString()` and `AddReferencedString()` methods

### Tokenization Logic
- `Tokenizer.ParseTokens()` splits .NET identifiers into meaningful words
- Handles PascalCase, dot notation, and all-caps acronyms
- Special cases: "AspNetCore" remains intact, digits/symbols trigger different logic

### CSV Rule Format
- **TypeRenames.csv**: `TokenHandle,NewName` 
- **MethodRenames.csv**: `TokenHandle,NewName`
- **TypeRenamesByName.csv**: `OldName,NewName` (alternative format)

## File Structure Conventions

- `/data/` - Contains summary CSV files for different .NET shared frameworks
- `/out/` - Default output directory for processed assemblies
- `/auxillary/` - Subdirectory with string analysis (MetadataStrings.csv, MetadataWords.csv)
- `en.sqlite3` - English dictionary for part-of-speech analysis

## Testing Patterns

- Use `[DataTestMethod]` with `[DynamicData]` for tokenizer test cases
- Test data provided via static properties returning `IEnumerable<object[]>`
- Focus on edge cases: empty strings, special characters, mixed casing

## Development Notes

- Uses .slnx solution format (XML-based)
- PowerShell scripts handle bulk operations on .NET shared frameworks  
- Word frequency analysis integrates with English dictionary for linguistic insights
- Some metadata table exports are commented out in `MetadataExporter.DumpMetadata()`