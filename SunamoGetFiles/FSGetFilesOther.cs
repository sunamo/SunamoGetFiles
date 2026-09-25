namespace SunamoGetFiles;

partial class FSGetFiles
{
    public static List<string> FilesOfExtension(ILogger logger, string folder, string extension)
    {
        return GetFilesEveryFolder(logger, folder, "*." + extension, SearchOption.TopDirectoryOnly);
    }

    public static List<string> FilesOfExtensionsArray(string folder, List<string> extensions)
    {
        var foundFiles = new List<string>();
        FS.NormalizeExtensions(extensions);
        var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            var extension = FS.GetNormalizedExtension(item);
            if (extensions.Contains(extension)) foundFiles.Add(item);
        }

        return foundFiles;
    }

    public static string GetFilesSize(List<string> files)
    {
        long size = 0;
        foreach (var item in files)
        {
            var fileInfo = new FileInfo(item);
            size += fileInfo.Length;
        }

        return FS.GetSizeInAutoString(size);
    }


    public static
        async Task<List<string>>
        FilesWhichContainsAll(ILogger logger, object source, string mask, IList<string> list)
    {
        var listCount = list.Count();
        var result = new List<string>();
        IList<string>? sourceFiles = null;
        if (source is IList<string>)
            sourceFiles = (IList<string>)source;
        else
            sourceFiles = GetFilesEveryFolder(logger, source.ToString() ?? string.Empty, mask, true);
        foreach (var item in sourceFiles)
        {
            var fileContent =
                await FileAsync.ReadAllTextAsync(item);
            if (list.Where(text => fileContent.Contains(text)).Count() ==
                listCount) result.Add(item);
        }

        return result;
    }

    public static FileInfo[] GetFileInfosOfExtensions(string folder, SearchOption searchOption, params string[] extensions)
    {
        var result = new List<FileInfo>();
        var directoryInfo = new DirectoryInfo(folder);
        foreach (var item in extensions) result.AddRange(directoryInfo.GetFiles("*" + item, searchOption));
        return result.ToArray();
    }

    public static List<string> AllFilesInFolders(ILogger logger, IList<string> folders, IList<string> extensions, SearchOption searchOption,
        GetFilesEveryFolderArgs? args = null)
    {
        var files = new List<string>();
        foreach (var item in folders)
            foreach (var extension in extensions)
                files.AddRange(GetFilesEveryFolder(logger, item, FS.MaskFromExtension(extension), searchOption, args));
        return files;
    }

    public static
    async Task<Dictionary<string, string>>
    GetFilesWithContentInDictionary(ILogger logger, string folder, string mask, SearchOption searchOption)
    {
        var result = new Dictionary<string, string>();
        var files = GetFilesEveryFolder(logger, folder, mask, searchOption);
        foreach (var file in files)
            result.Add(file,
                await FileAsync.ReadAllTextAsync(file));
        return result;
    }

    public static List<string> GetFilesOfExtensions(ILogger logger, string folder, SearchOption searchOption, params string[] extensions)
    {
        var result = new List<string>();
        foreach (var item in extensions) result.AddRange(GetFilesEveryFolder(logger, folder, "*" + item, searchOption));
        return result;
    }
}
