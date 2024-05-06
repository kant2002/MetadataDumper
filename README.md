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

# API Comprehension

Let's say your English is A1, and for simpliciy assume that all words you are know are from the subset used by .NET

Take number from this answer https://languagelearning.stackexchange.com/questions/3061/what-are-estimates-of-vocabulary-size-for-each-cefr-level about possible A1 vocabulary sizes: 300, 600, 1500

Let's build what % of API can be comprehensible for person which know meaning of some words.

## Core API

| Vocabulary size | % of API surface comprehensible |
| --------------: | ------------------------------: |
| 50 | 36% |
| 100 | 48% |
| 108 | 50% |
| 300 | 70% |
| 383 | 75% |
| 600 | 82% |
| 1500 | 94% |

As you can see, if you choose 50 words wisely, you can already understand 36% of APIs in basic C#, 
assuming obviously that you understand programming concepts already. And if you choose 108 words, you can read 50% of API surface.

## ASP.NET Core

| Vocabulary size | % of API surface comprehensible |
| --------------: | ------------------------------: |
| 50 | 35% |
| 100 | 47% |
| 112 | 50% |
| 300 | 70% |
| 384 | 75% |
| 600 | 82% |
| 1500 | 94% |

## With Desktop

| Vocabulary size | % of API surface comprehensible |
| --------------: | ------------------------------: |
| 50 | 33% |
| 100 | 44% |
| 138 | 50% |
| 300 | 65% |
| 486 | 75% |
| 600 | 78% |
| 1500 | 91% |


# References

- https://download.wikdict.com/dictionaries/sqlite/2_2024-03/