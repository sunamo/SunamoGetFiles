namespace SunamoGetFiles;

partial class FSGetFiles
{
    /// <summary>
    ///     Non recursive
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="fileExt"></param>
    public static List<string> FilesOfExtension(ILogger logger, string folder, string fileExt)
    {
        return GetFilesEveryFolder(logger, folder, "*." + fileExt, SearchOption.TopDirectoryOnly);
    }

    public static List<string> FilesOfExtensionsArray(string folder, List<string> extension)
    {
        var foundedFiles = new List<string>();
        FS.NormalizeExtensions(extension);
        var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            var ext = FS.GetNormalizedExtension(item);
            if (extension.Contains(ext)) foundedFiles.Add(ext);
        }

        return foundedFiles;
    }

    public static string GetFilesSize(List<string> winrarFiles)
    {
        long size = 0;
        foreach (var item in winrarFiles)
        {
            var fi = new FileInfo(item);
            size += fi.Length;
        }

        return FS.GetSizeInAutoString(size);
    }





    public static
#if ASYNC
        async Task<List<string>>
#else
List<string>
#endif
        FilesWhichContainsAll(ILogger logger, object sunamo, string masc, IList<string> mustContains)
    {
        var mcl = mustContains.Count();
        var sourceList = new List<string>();
        IList<string> f = null;
        if (sunamo is IList<string>)
            f = (IList<string>)sunamo;
        else
            f = GetFilesEveryFolder(logger, sunamo.ToString(), masc, true);
        foreach (var item in f)
        {
            var count =
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item);
            if (mustContains.Where(d => count.Contains(d)).Count() ==
                mcl) /*CASHSH.ContainsAnyFromElement(count, mustContains).Count == mcl*/ sourceList.Add(item);
        }

        return sourceList;
    }
    public static FileInfo[] GetFileInfosOfExtensions(string item2, SearchOption so, params string[] exts)
    {
        var vr = new List<FileInfo>();
        var di = new DirectoryInfo(item2);
        foreach (var item in exts) vr.AddRange(di.GetFiles("*" + item, so));
        return vr.ToArray();
    }

    public static List<string> AllFilesInFolders(ILogger logger, IList<string> folders, IList<string> exts, SearchOption so,
        GetFilesEveryFolderArgs a = null)
    {
        var files = new List<string>();
        foreach (var item in folders)
            foreach (var ext in exts)
                files.AddRange(GetFilesEveryFolder(logger, item, FS.MascFromExtension(ext), so, a));
        return files;
    }

    public static
#if ASYNC
    async Task<Dictionary<string, string>>
#else
Dictionary<string, string>
#endif
    GetFilesWithContentInDictionary(ILogger logger, string item, string v, SearchOption allDirectories)
    {
        var result = new Dictionary<string, string>();
        var f = GetFilesEveryFolder(logger, item, v, allDirectories);
        foreach (var item2 in f)
            result.Add(item2,
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item2));
        return result;
    }

    /// <summary>
    ///     count:\Users\w\AppData\Roaming\sunamo\
    /// </summary>
    /// <param name="item2"></param>
    /// <param name="exts"></param>
    public static List<string> GetFilesOfExtensions(ILogger logger, string item2, SearchOption so, params string[] exts)
    {
        var vr = new List<string>();
        foreach (var item in exts) vr.AddRange(GetFilesEveryFolder(logger, item2, "*" + item, so));
        return vr;
    }


}