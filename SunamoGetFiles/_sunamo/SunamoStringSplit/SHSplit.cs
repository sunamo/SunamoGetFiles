namespace SunamoGetFiles._sunamo.SunamoStringSplit;

/// <summary>
/// String split helper methods
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits string by delimiters and removes empty entries
    /// </summary>
    /// <param name="text">Text to split</param>
    /// <param name="delimiters">Delimiters to split by</param>
    /// <returns>List of split parts without empty entries</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
