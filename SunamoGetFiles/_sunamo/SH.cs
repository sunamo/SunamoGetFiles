namespace SunamoGetFiles._sunamo;

/// <summary>
/// String helper methods
/// </summary>
internal class SH
{
    /// <summary>
    /// Converts first character of string to uppercase
    /// </summary>
    /// <param name="text">Input text</param>
    /// <returns>Text with first character in uppercase</returns>
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