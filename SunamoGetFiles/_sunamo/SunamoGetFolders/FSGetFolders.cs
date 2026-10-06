namespace SunamoGetFiles._sunamo.SunamoGetFolders;

internal class FSGetFolders
{
    internal static void GetFoldersEveryFolder(ILogger logger, List<string> result, string folder, string searchPattern, GetFoldersEveryFolderArgs args)
    {
        if (logger == null)
        {
            throw new ArgumentNullException("logger");
        }
        try
        {
            var subdirectories = Directory.GetDirectories(folder, searchPattern, SearchOption.TopDirectoryOnly).ToList();
            if (args.IgnoreFoldersWithName != null)
            {
                for (int i = subdirectories.Count - 1; i >= 0; i--)
                {
                    if (args.IgnoreFoldersWithName.Contains(FS.GetFileName(subdirectories[i])))
                    {
                        subdirectories.RemoveAt(i);
                    }
                }
            }
            result.AddRange(subdirectories);
            foreach (var item in subdirectories)
            {
                GetFoldersEveryFolder(logger, result, item, searchPattern, args);
            }
        }
        catch (Exception ex)
        {
            if (args.ThrowException)
            {
                throw;
            }
            else
            {
                logger.LogError(message: ex.Message);
            }
        }
    }
}
