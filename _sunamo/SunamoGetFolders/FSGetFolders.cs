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