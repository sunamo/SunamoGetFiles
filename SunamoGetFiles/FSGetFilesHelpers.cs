namespace SunamoGetFiles;
partial class FSGetFiles
{
    public static void FilterByGetFilesArgs(List<string> list, IEnumerable<string> folders, GetFilesEveryFolderArgs a)
    {
        if (a == null) a = new GetFilesEveryFolderArgs();
        CAChangeContent.ChangeContent0(null, list, d => SH.FirstCharUpper(d));
        if (a._trimA1AndLeadingBs)
            foreach (var folder in folders)
                list = CAChangeContent.ChangeContent0(null, list, d => d = d.Replace(folder, "").TrimEnd('\\'));
        if (a._trimExt)
            foreach (var folder in folders)
                list = CAChangeContent.ChangeContent0(null, list, d => d = SHParts.RemoveAfterLast(d, '.'));
        if (a.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in a.excludeFromLocationsCOntains)
                list = list.Where(d => !d.Contains(item)).ToList();
        //CA.RemoveWhichContains(list, item, false);
        Dictionary<string, DateTime> dictLastModified = null;
        var isLastModifiedFromFn = a.LastModifiedFromFn != null;
        if (a.dontIncludeNewest || a.byDateOfLastModifiedAsc || isLastModifiedFromFn)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? dt = null;
                if (isLastModifiedFromFn) dt = a.LastModifiedFromFn(Path.GetFileNameWithoutExtension(item));
                if (!dt.HasValue) dt = FS.LastModified(item);
                dictLastModified.Add(item, dt.Value);
            }

            list = dictLastModified.OrderBy(t => t.Value).Select(r => r.Key).ToList();
        }

        if (a.dontIncludeNewest) list.RemoveAt(list.Count - 1);
        if (a.excludeWithMethod != null) a.excludeWithMethod.Invoke(list);
    }
}
