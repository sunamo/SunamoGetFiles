
namespace SunamoGetFiles._sunamo.SunamoGetFolders;
using Microsoft.Extensions.Logging;

internal class FSGetFolders
{
    internal static void GetFoldersEveryFolder(List<string> folders, string folder, string v, GetFoldersEveryFolderArgs e)
    {
        if (e.Logger == null)
        {
            ArgumentNullException.ThrowIfNull(e.Logger, "e.Logger");
        }

        try
        {
            var d = Directory.GetDirectories(folder, v, SearchOption.TopDirectoryOnly).ToList();

            if (e.IgnoreFoldersWithName != null)
            {
                for (int i = d.Count - 1; i >= 0; i--)
                {
                    if (e.IgnoreFoldersWithName.Contains(FS.GetFileName(d[i])))
                    {
                        d.RemoveAt(i);
                    }
                }
            }

            folders.AddRange(d);
            foreach (var item in d)
            {
                GetFoldersEveryFolder(folders, item, v, e);
            }
        }
        catch (Exception ex)
        {

            if (e.throwEx)
            {
                throw ex;
            }
            else
            {

                e.Logger.LogError(Exceptions.TextOfExceptions(ex));
                // do nothing
                //return new List<string>();
            }
        }
    }
}