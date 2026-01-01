namespace SunamoGetFiles;

/// <summary>
/// Provides methods for getting files by extensions
/// </summary>
partial class FSGetFiles
{
    /// <summary>
    /// Gets files grouped by extensions.
    /// Returns dictionary where keys are normalized extensions.
    /// Extensions with zero files will not be included in the dictionary.
    /// To get all extensions in a folder, use FS.AllExtensionsInFolders method.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Folder path to search</param>
    /// <param name="args">Optional arguments for file search</param>
    /// <param name="extensionsWithDot">Extensions to search for (with dot, e.g., ".txt", ".cs")</param>
    /// <returns>Dictionary where keys are normalized extensions and values are lists of file paths</returns>
    public static Dictionary<string, List<string>> FilesOfExtensions(ILogger logger, string folder, GetFilesEveryFolderArgs args, params string[] extensionsWithDot)
    {
        var dict = new Dictionary<string, List<string>>();
        foreach (var item in extensionsWithDot)
        {
            var ext = FS.NormalizeExtension(item);
            var files = GetFilesEveryFolder(logger, folder, "*" + ext, SearchOption.AllDirectories, args);
            if (files.Count != 0) dict.Add(ext, files);
        }
        return dict;
    }
}
