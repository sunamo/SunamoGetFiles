namespace SunamoGetFiles._sunamo.SunamoGetFolders;

/// <summary>
/// Folder system helper methods
/// </summary>
internal class FSGetFolders
{
    /// <summary>
    /// Gets folders recursively from every folder
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folders">Output list of folders</param>
    /// <param name="folder">Root folder to search</param>
    /// <param name="searchPattern">Search pattern for folders</param>
    /// <param name="args">Arguments for folder search</param>
    internal static void GetFoldersEveryFolder(ILogger logger, List<string> folders, string folder, string searchPattern, GetFoldersEveryFolderArgs args)
    {
        if (logger == null)
        {
            ArgumentNullException.ThrowIfNull(logger, "logger");
        }
        try
        {
            var data = Directory.GetDirectories(folder, searchPattern, SearchOption.TopDirectoryOnly).ToList();
            if (args.IgnoreFoldersWithName != null)
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (args.IgnoreFoldersWithName.Contains(FS.GetFileName(data[i])))
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            folders.AddRange(data);
            foreach (var item in data)
            {
                GetFoldersEveryFolder(logger, folders, item, searchPattern, args);
            }
        }
        catch (Exception ex)
        {
            if (args.ThrowEx)
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
