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
    /// <param name="result">Output list of folders</param>
    /// <param name="folder">Root folder to search</param>
    /// <param name="searchPattern">Search pattern for folders</param>
    /// <param name="args">Arguments for folder search</param>
    internal static void GetFoldersEveryFolder(ILogger logger, List<string> result, string folder, string searchPattern, GetFoldersEveryFolderArgs args)
    {
        if (logger == null)
        {
            ArgumentNullException.ThrowIfNull(logger, "logger");
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