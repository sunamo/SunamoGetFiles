namespace SunamoGetFiles.Enums;

// variables names: ok
/// <summary>
/// Computer file size units for automatic size conversion
/// </summary>
public enum ComputerSizeUnitsGetFiles : byte
{
    /// <summary>
    /// Automatically determines the most appropriate unit
    /// </summary>
    Auto = 0,

    /// <summary>
    /// Bytes
    /// </summary>
    B = 1,

    /// <summary>
    /// Kilobytes (1024 bytes)
    /// </summary>
    KB = 2,

    /// <summary>
    /// Megabytes (1024 KB)
    /// </summary>
    MB = 3,

    /// <summary>
    /// Gigabytes (1024 MB)
    /// </summary>
    GB = 4,

    /// <summary>
    /// Terabytes (1024 GB)
    /// </summary>
    TB = 5
}