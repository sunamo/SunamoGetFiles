namespace SunamoGetFiles._sunamo.SunamoArgs;

/// <summary>
/// Arguments for changing content operations
/// </summary>
internal class ChangeContentArgsGetFiles
{
    /// <summary>
    /// Whether to remove null values
    /// </summary>
    internal bool RemoveNull { get; set; } = false;

    /// <summary>
    /// Whether to remove empty strings
    /// </summary>
    internal bool RemoveEmpty { get; set; } = false;

    /// <summary>
    /// Whether to switch first and second arguments
    /// </summary>
    internal bool SwitchFirstAndSecondArg { get; set; } = false;
}