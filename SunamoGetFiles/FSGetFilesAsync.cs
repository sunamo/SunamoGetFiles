namespace SunamoGetFiles;

/// <summary>
/// Provides asynchronous methods for getting files from the file system
/// </summary>
partial class FSGetFiles
{
    /// <summary>
    /// Gets file sizes for a list of files
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="files">List of file paths</param>
    /// <returns>List of file sizes in bytes</returns>
    public static List<long> GetFilesSizes(ILogger logger, List<string> files)
    {
        var sizes = new List<long>();
        foreach (var item in files)
        {
            try
            {
                sizes.Add(new FileInfo(item).Length);
            }
            catch (Exception ex)
            {
                logger.LogError(Exceptions.TextOfExceptions(ex));
            }
        }
        return sizes;
    }

    /// <summary>
    /// Gets files from specified folder(s) with mask and search options.
    /// When Access Denied exception occurs, use GetFilesEveryFolder which searches in every subfolder.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Folder path (can be semicolon-delimited for multiple folders)</param>
    /// <param name="mask">File mask (use GetFilesOfExtensions for multiple extensions)</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <param name="args">Optional arguments for file search</param>
    /// <returns>List of file paths</returns>
    public static Task<List<string>> GetFilesAsync(ILogger logger, string folder, string mask, SearchOption searchOption,
        GetFilesEveryFolderArgs? args = null)
    {
        if (!Directory.Exists(folder) && !folder.Contains(";"))
            return Task.FromResult(new List<string>());

        if (args == null!) args = new GetFilesEveryFolderArgs();

        var folders = SHSplit.Split(folder, ";");
        for (var i = 0; i < folders.Count; i++)
            folders[i] = folders[i].TrimEnd('\\') + "\\";

        var list = new List<string>();
        foreach (var currentFolder in folders)
        {
            if (Directory.Exists(currentFolder))
            {
                return Task.FromResult(GetFilesEveryFolder(logger, currentFolder, mask, searchOption));
            }
        }

        for (var i = 0; i < list.Count; i++)
            list[i] = SH.FirstCharUpper(list[i]);

        if (args.TrimRootFolderAndLeadingBackslashes)
        {
            foreach (var currentFolder in folders)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    list[i] = list[i].Replace(currentFolder, "");
                    list[i] = SHParts.RemoveAfterLast(list[i], '.');
                }
            }
        }

        if (args.ExcludeFromLocationsContains != null)
        {
            foreach (var item in args.ExcludeFromLocationsContains)
                list = list.Where(filePath => !filePath.Contains(item)).ToList();
        }

        Dictionary<string, DateTime>? lastModifiedByFile = null;
        var hasLastModifiedFromFileName = args.LastModifiedFromFileName != null;
        if (args.DontIncludeNewest || args.ByDateOfLastModifiedAsc || hasLastModifiedFromFileName)
        {
            lastModifiedByFile = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? lastModified = null;
                if (hasLastModifiedFromFileName)
                    lastModified = args.LastModifiedFromFileName?.Invoke(Path.GetFileNameWithoutExtension(item));
                if (!lastModified.HasValue)
                    lastModified = FS.LastModified(item);
                lastModifiedByFile.Add(item, lastModified.Value);
            }

            list = lastModifiedByFile.OrderBy(pair => pair.Value).Select(pair => pair.Key).ToList();
        }

        if (args.DontIncludeNewest)
            list.RemoveAt(list.Count - 1);

        if (args.ExcludeWithMethod != null)
            args.ExcludeWithMethod?.Invoke(list);

        return Task.FromResult(list);
    }
}