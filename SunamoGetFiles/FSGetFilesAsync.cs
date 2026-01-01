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
    /// Gets files asynchronously from specified folder(s) with mask and search options.
    /// When Access Denied exception occurs, use GetFilesEveryFolder which searches in every subfolder.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Folder path (can be semicolon-delimited for multiple folders)</param>
    /// <param name="mask">File mask (use GetFilesOfExtensions for multiple extensions)</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <param name="getFilesArgs">Optional arguments for file search</param>
    /// <returns>List of file paths</returns>
    public static async Task<List<string>> GetFilesAsync(ILogger logger, string folder, string mask, SearchOption searchOption,
        GetFilesEveryFolderArgs getFilesArgs = null)
    {
        if (!Directory.Exists(folder) && !folder.Contains(";"))
            return new List<string>();

        if (getFilesArgs == null) getFilesArgs = new GetFilesEveryFolderArgs();

        var folders = SHSplit.Split(folder, ";");
        for (var i = 0; i < folders.Count; i++)
            folders[i] = folders[i].TrimEnd('\\') + "\"";

        var list = new List<string>();
        foreach (var currentFolder in folders)
        {
            if (Directory.Exists(currentFolder))
            {
                return GetFilesEveryFolder(logger, currentFolder, mask, searchOption);
            }
        }

        for (var i = 0; i < list.Count; i++)
            list[i] = SH.FirstCharUpper(list[i]);

        if (getFilesArgs.TrimRootFolderAndLeadingBackslashes)
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

        if (getFilesArgs.ExcludeFromLocationsContains != null)
        {
            foreach (var item in getFilesArgs.ExcludeFromLocationsContains)
                list = list.Where(filePath => !filePath.Contains(item)).ToList();
        }

        Dictionary<string, DateTime> dictLastModified = null;
        var isLastModifiedFromFn = getFilesArgs.LastModifiedFromFn != null;
        if (getFilesArgs.DontIncludeNewest || getFilesArgs.ByDateOfLastModifiedAsc || isLastModifiedFromFn)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? lastModified = null;
                if (isLastModifiedFromFn)
                    lastModified = getFilesArgs.LastModifiedFromFn(Path.GetFileNameWithoutExtension(item));
                if (!lastModified.HasValue)
                    lastModified = FS.LastModified(item);
                dictLastModified.Add(item, lastModified.Value);
            }

            list = dictLastModified.OrderBy(pair => pair.Value).Select(pair => pair.Key).ToList();
        }

        if (getFilesArgs.DontIncludeNewest)
            list.RemoveAt(list.Count - 1);

        if (getFilesArgs.ExcludeWithMethod != null)
            getFilesArgs.ExcludeWithMethod.Invoke(list);

        return list;
    }
}
