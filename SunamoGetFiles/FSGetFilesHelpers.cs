namespace SunamoGetFiles;

/// <summary>
/// Provides helper methods for file operations
/// </summary>
partial class FSGetFiles
{
    /// <summary>
    /// Filters file list according to GetFilesEveryFolderArgs settings
    /// </summary>
    /// <param name="list">List of file paths to filter</param>
    /// <param name="folders">Folders to process</param>
    /// <param name="args">Arguments containing filter settings</param>
    public static void FilterByGetFilesArgs(List<string> list, IEnumerable<string> folders, GetFilesEveryFolderArgs args)
    {
        if (args == null) args = new GetFilesEveryFolderArgs();

        CAChangeContent.ChangeContent0(null, list, filePath => SH.FirstCharUpper(filePath));

        if (args.TrimRootFolderAndLeadingBackslashes)
            foreach (var folder in folders)
                list = CAChangeContent.ChangeContent0(null, list, filePath => filePath = filePath.Replace(folder, "").TrimEnd('\\'));

        if (args.TrimExtension)
            foreach (var folder in folders)
                list = CAChangeContent.ChangeContent0(null, list, filePath => filePath = SHParts.RemoveAfterLast(filePath, '.'));

        if (args.ExcludeFromLocationsContains != null)
        {
            foreach (var item in args.ExcludeFromLocationsContains)
                list = list.Where(filePath => !filePath.Contains(item)).ToList();
        }

        Dictionary<string, DateTime> dictLastModified = null;
        var isLastModifiedFromFn = args.LastModifiedFromFn != null;
        if (args.DontIncludeNewest || args.ByDateOfLastModifiedAsc || isLastModifiedFromFn)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? lastModified = null;
                if (isLastModifiedFromFn)
                    lastModified = args.LastModifiedFromFn(Path.GetFileNameWithoutExtension(item));
                if (!lastModified.HasValue)
                    lastModified = FS.LastModified(item);
                dictLastModified.Add(item, lastModified.Value);
            }

            list = dictLastModified.OrderBy(pair => pair.Value).Select(pair => pair.Key).ToList();
        }

        if (args.DontIncludeNewest)
            list.RemoveAt(list.Count - 1);

        if (args.ExcludeWithMethod != null)
            args.ExcludeWithMethod.Invoke(list);
    }
}
