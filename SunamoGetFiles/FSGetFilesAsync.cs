namespace SunamoGetFiles;

partial class FSGetFiles
{
    /// <summary>
    /// Get files sizes.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="files">The files.</param>
    /// <returns>The get files sizes result.</returns>
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
    /// Get files async.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="folder">The folder.</param>
    /// <param name="mask">The mask.</param>
    /// <param name="searchOption">The search option.</param>
    /// <param name="args">The args.</param>
    /// <returns>The get files async result.</returns>
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
