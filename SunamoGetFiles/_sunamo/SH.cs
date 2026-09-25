namespace SunamoGetFiles._sunamo;

internal class SH
{
    internal static string FirstCharUpper(string text)
    {
        if (text.Length == 1)
        {
            return text.ToUpper();
        }

        string restOfString = text.Substring(1);
        return text[0].ToString().ToUpper() + restOfString;
    }
}
