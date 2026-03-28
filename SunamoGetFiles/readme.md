### SunamoGetFiles

A .NET library for retrieving files from the file system with automatic exception handling, recursive folder traversal, progress bar support, and flexible filtering options.

Part of PlatformIndependentNuGetPackages:

- [nuget.org](https://www.nuget.org/profiles/sunamo)
- [github.org](https://github.com/sunamo/PlatformIndependentNuGetPackages)

Another links:

- [Developer site](https://sunamo.cz)

Request for new features / bug report / etc: [Mail](mailto:radek.jancik@sunamo.cz) or on GitHub

## Key Features

- Recursive file search with per-folder exception handling
- Semicolon-delimited multi-folder support
- Progress bar callbacks for UI integration
- Flexible filtering (by extension, content, location, date)
- Automatic file size formatting (B/KB/MB/GB/TB)
- Junction point awareness

## Target Frameworks

**TargetFrameworks:** `net10.0;net9.0;net8.0`

**Reason:** Code uses C# 12.0 features (collection expressions) requiring .NET 8.0+:
- Collection expressions `[]` syntax requires C# 12.0 (net8.0+)
