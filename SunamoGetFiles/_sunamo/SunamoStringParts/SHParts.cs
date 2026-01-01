namespace SunamoGetFiles._sunamo.SunamoStringParts;

/// <summary>
/// String parts helper methods
/// </summary>
internal class SHParts
{
    /// <summary>
    /// Removes part of string after last occurrence of delimiter
    /// </summary>
    /// <param name="text">Input text</param>
    /// <param name="delimiter">Delimiter to search for</param>
    /// <returns>String without part after last delimiter</returns>
    internal static string RemoveAfterLast(string text, object delimiter)
    {
        int lastIndex = text.LastIndexOf(delimiter.ToString());
        if (lastIndex != -1)
        {
            string result = text.Substring(0, lastIndex);
            return result;
        }
        return text;
    }
}
