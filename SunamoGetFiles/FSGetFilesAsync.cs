namespace SunamoGetFiles;

partial class FSGetFiles
{
    public static List<long> GetFilesSizes(ILogger logger, List<string> f)
    {
        var sizes = new List<long>();
        foreach (var item in f)
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
    ///     When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    ///     A1 have to be with ending backslash
    ///     A4 must have underscore otherwise is suggested while I try type true
    ///     A2 can be delimited by semicolon. In case more extension use GetFilesOfExtensions
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    /// <param name="searchOption"></param>
    public static async Task<List<string>> GetFilesAsync(ILogger logger, string folder2, string mask, SearchOption searchOption,
        GetFilesEveryFolderArgs getFilesArgs = null)
    {
        if (!Directory.Exists(folder2) && !folder2.Contains(";"))
            //ThisApp.Warning(folder2 + "does not exists");
            return new List<string>();
        if (getFilesArgs == null) getFilesArgs = new GetFilesEveryFolderArgs();
        var folders = SHSplit.Split(folder2, ";");
        for (var i = 0; i < folders.Count; i++) folders[i] = folders[i].TrimEnd('\\') + "\"";
        //CA.PostfixIfNotEnding("\"", folders);
        var list = new List<string>();
        foreach (var folder in folders)
            if (!Directory.Exists(folder))
            {
            }
            else
            {
                return GetFilesEveryFolder(logger, folder, mask, searchOption);
            }

        //CAChangeContent.ChangeContent0(null, list, d => SH.FirstCharUpper(d));
        for (var i = 0; i < list.Count; i++) list[i] = SH.FirstCharUpper(list[i]);
        if (getFilesArgs._trimA1AndLeadingBs)
            foreach (var folder in folders)
                //list = CAChangeContent.ChangeContent0(null, list, d => d = d.Replace(folder, ""));
                for (var i = 0; i < list.Count; i++)
                    list[i] = list[i].Replace(folder, "");
        if (getFilesArgs._trimA1AndLeadingBs)
            foreach (var folder in folders)
            {
                list = CAChangeContent.ChangeContent0(null, list, d => d = SHParts.RemoveAfterLast(d, '.'));
                for (var i = 0; i < list.Count; i++) list[i] = SHParts.RemoveAfterLast(list[i], '.');
            }

        if (getFilesArgs.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in getFilesArgs.excludeFromLocationsCOntains)
                list = list.Where(d => !d.Contains(item)).ToList();
        //CA.RemoveWhichContains(list, item, false);
        Dictionary<string, DateTime> dictLastModified = null;
        var isLastModifiedFromFn = getFilesArgs.LastModifiedFromFn != null;
        if (getFilesArgs.dontIncludeNewest || getFilesArgs.byDateOfLastModifiedAsc || isLastModifiedFromFn)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? dt = null;
                if (isLastModifiedFromFn) dt = getFilesArgs.LastModifiedFromFn(Path.GetFileNameWithoutExtension(item));
                if (!dt.HasValue) dt = FS.LastModified(item);
                dictLastModified.Add(item, dt.Value);
            }

            list = dictLastModified.OrderBy(t => t.Value).Select(r => r.Key).ToList();
        }

        if (getFilesArgs.dontIncludeNewest) list.RemoveAt(list.Count - 1);
        if (getFilesArgs.excludeWithMethod != null) getFilesArgs.excludeWithMethod.Invoke(list);
        return list;
    }


}