namespace SunamoGetFiles._sunamo.SunamoLang.SunamoI18N;

/// <summary>
/// Translation helper methods
/// </summary>
internal class Translate
{
    /// <summary>
    /// Gets translation from key.
    /// Currently returns the key itself (no actual translation).
    /// Usage example: Exceptions.IsNotWindowsPathFormat
    /// </summary>
    /// <param name="key">Translation key</param>
    /// <returns>Translation (currently just the key)</returns>
    internal static string FromKey(string key)
    {
        return key;
    }
}