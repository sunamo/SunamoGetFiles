namespace SunamoGetFiles._sunamo.SunamoStringParts;

internal class SHParts
{
    internal static string RemoveAfterLast(string text, object delimiter)
    {
        int lastIndex = text.LastIndexOf(delimiter.ToString() ?? string.Empty);
        if (lastIndex != -1)
        {
            return text.Substring(0, lastIndex);
        }
        return text;
    }
}
