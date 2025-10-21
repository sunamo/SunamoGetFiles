// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoGetFiles._sunamo.SunamoGetFolders;
internal class FSGetFolders
{
    internal static void GetFoldersEveryFolder(ILogger logger, List<string> folders, string folder, string v, GetFoldersEveryFolderArgs e)
    {
        if (logger == null)
        {
            ArgumentNullException.ThrowIfNull(logger, "logger");
        }
        try
        {
            var data = Directory.GetDirectories(folder, v, SearchOption.TopDirectoryOnly).ToList();
            if (e.IgnoreFoldersWithName != null)
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (e.IgnoreFoldersWithName.Contains(FS.GetFileName(data[i])))
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            folders.AddRange(data);
            foreach (var item in data)
            {
                GetFoldersEveryFolder(logger, folders, item, v, e);
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
                logger.LogError(message: ex.Message);
                // do nothing
                //return new List<string>();
            }
        }
    }
}