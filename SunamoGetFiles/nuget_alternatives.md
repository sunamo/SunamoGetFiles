# NuGet Alternatives to SunamoGetFiles

This document lists popular NuGet packages that provide similar functionality to SunamoGetFiles.

## Overview

File enumeration utilities

## Alternative Packages

### System.IO.Directory
- **NuGet**: System.IO.FileSystem
- **Purpose**: Built-in file enumeration
- **Key Features**: GetFiles, EnumerateFiles, search patterns

### Microsoft.Extensions.FileSystemGlobbing
- **NuGet**: Microsoft.Extensions.FileSystemGlobbing
- **Purpose**: Glob pattern matching
- **Key Features**: Pattern-based file search, includes/excludes

### DotNet.Glob
- **NuGet**: DotNet.Glob
- **Purpose**: Glob matching
- **Key Features**: Fast glob pattern evaluation

### System.IO.Abstractions
- **NuGet**: System.IO.Abstractions
- **Purpose**: Testable file enumeration
- **Key Features**: Mockable directory operations

## Comparison Notes

Directory.EnumerateFiles for standard use. FileSystemGlobbing for complex patterns.

## Choosing an Alternative

Consider these alternatives based on your specific needs:
- **System.IO.Directory**: Built-in file enumeration
- **Microsoft.Extensions.FileSystemGlobbing**: Glob pattern matching
- **DotNet.Glob**: Glob matching
