namespace SunamoGetFiles;

partial class FSGetFiles
{
    /// <summary>
    /// Gets files with specified extension from folder (non-recursive)
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Folder path to search</param>
    /// <param name="extension">File extension without dot (e.g., "txt", "cs")</param>
    /// <returns>List of file paths with specified extension</returns>
    public static List<string> FilesOfExtension(ILogger logger, string folder, string extension)
    {
        return GetFilesEveryFolder(logger, folder, "*." + extension, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// Gets files with any of specified extensions from folder recursively
    /// </summary>
    /// <param name="folder">Folder path to search</param>
    /// <param name="extensions">List of extensions to search for</param>
    /// <returns>List of file paths with any of specified extensions</returns>
    public static List<string> FilesOfExtensionsArray(string folder, List<string> extensions)
    {
        var foundFiles = new List<string>();
        FS.NormalizeExtensions(extensions);
        var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            var extension = FS.GetNormalizedExtension(item);
            if (extensions.Contains(extension)) foundFiles.Add(extension);
        }

        return foundFiles;
    }

    /// <summary>
    /// Gets total size of files in human-readable format
    /// </summary>
    /// <param name="files">List of file paths</param>
    /// <returns>Total size in automatically determined unit (B, KB, MB, GB, TB)</returns>
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





    /// <summary>
    /// Gets files which contain all specified strings in their content
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="source">Source folder path or list of file paths</param>
    /// <param name="mask">File mask pattern</param>
    /// <param name="mustContains">List of strings that must all be present in file content</param>
    /// <returns>List of file paths that contain all specified strings</returns>
    public static
#if ASYNC
        async Task<List<string>>
#else
List<string>
#endif
        FilesWhichContainsAll(ILogger logger, object source, string mask, IList<string> mustContains)
    {
        var mustContainsCount = mustContains.Count();
        var result = new List<string>();
        IList<string>? files = null;
        if (source is IList<string>)
            files = (IList<string>)source;
        else
            files = GetFilesEveryFolder(logger, source.ToString() ?? string.Empty, mask, true);
        foreach (var item in files)
        {
            var fileContent =
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item);
            if (mustContains.Where(text => fileContent.Contains(text)).Count() ==
                mustContainsCount) result.Add(item);
        }

        return result;
    }
    /// <summary>
    /// Gets FileInfo array for files with specified extensions
    /// </summary>
    /// <param name="folder">Folder path to search</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <param name="extensions">Extensions to search for</param>
    /// <returns>Array of FileInfo objects</returns>
    public static FileInfo[] GetFileInfosOfExtensions(string folder, SearchOption searchOption, params string[] extensions)
    {
        var result = new List<FileInfo>();
        var directoryInfo = new DirectoryInfo(folder);
        foreach (var item in extensions) result.AddRange(directoryInfo.GetFiles("*" + item, searchOption));
        return result.ToArray();
    }

    /// <summary>
    /// Gets all files from multiple folders with specified extensions
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folders">List of folder paths to search</param>
    /// <param name="extensions">List of file extensions</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <param name="args">Optional arguments for file search</param>
    /// <returns>List of all file paths from all folders with specified extensions</returns>
    public static List<string> AllFilesInFolders(ILogger logger, IList<string> folders, IList<string> extensions, SearchOption searchOption,
        GetFilesEveryFolderArgs? args = null)
    {
        var files = new List<string>();
        foreach (var item in folders)
            foreach (var extension in extensions)
                files.AddRange(GetFilesEveryFolder(logger, item, FS.MascFromExtension(extension), searchOption, args));
        return files;
    }

    /// <summary>
    /// Gets files with their content in dictionary
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Folder path to search</param>
    /// <param name="mask">File mask pattern</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <returns>Dictionary where keys are file paths and values are file contents</returns>
    public static
#if ASYNC
    async Task<Dictionary<string, string>>
#else
Dictionary<string, string>
#endif
    GetFilesWithContentInDictionary(ILogger logger, string folder, string mask, SearchOption searchOption)
    {
        var result = new Dictionary<string, string>();
        var files = GetFilesEveryFolder(logger, folder, mask, searchOption);
        foreach (var file in files)
            result.Add(file,
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(file));
        return result;
    }

    /// <summary>
    /// Gets files with any of specified extensions
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Folder path to search</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <param name="extensions">Extensions to search for</param>
    /// <returns>List of file paths with any of specified extensions</returns>
    public static List<string> GetFilesOfExtensions(ILogger logger, string folder, SearchOption searchOption, params string[] extensions)
    {
        var result = new List<string>();
        foreach (var item in extensions) result.AddRange(GetFilesEveryFolder(logger, folder, "*" + item, searchOption));
        return result;
    }


}