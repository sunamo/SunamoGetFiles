namespace SunamoGetFiles._sunamo;

internal class SH
{

    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
        {
            return nazevPP.ToUpper();
        }

        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }

}