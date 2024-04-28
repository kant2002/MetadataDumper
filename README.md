# MetadataDumper

Dump .NET metadata information into CSV files. Each metadata table saved to separate CSV file.

## Usage

```
dotnet run "C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App\8.0.4\Microsoft.AspNetCore.Authentication.Abstractions.dll" Microsoft.AspNetCore.Authentication.Abstractions
```

## Collect statistcis about words from .NET distribution

```
.\summary.ps1
```

## Dotnet API analysis

- Core .NET consists of unique 6,757 words or abbreviations, or typos. 676,627 usages of these words across APIs
- Core and Web related .NET consists of unique 7,366 words or abbreviations, or typos. 962,663 usages of these words across APIs
- Core .NET consists of unique 11,467 words or abbreviations, or typos. 1,881,143 usages of these words across APIs

Same in tabular data

| .NET variant | Words | Usages |
| -----------  | ----: | ------:|
| Core         | 6,757 | 676,627 |
| ASP.NET Core | 7,366 | 962,663 |
| With Desktop | 11,467 | 1,881,143 |