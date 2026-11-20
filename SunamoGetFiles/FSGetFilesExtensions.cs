namespace SunamoGetFiles;

partial class FSGetFiles
{
    /// <summary>
    /// Files of extensions.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="folder">The folder.</param>
    /// <param name="args">The args.</param>
    /// <param name="extensionsWithDot">The extensions with dot.</param>
    /// <returns>The files of extensions result.</returns>
    public static Dictionary<string, List<string>> FilesOfExtensions(ILogger logger, string folder, GetFilesEveryFolderArgs args, params string[] extensionsWithDot)
    {
        var filesByExtension = new Dictionary<string, List<string>>();
        foreach (var item in extensionsWithDot)
        {
            var extension = FS.NormalizeExtension(item);
            var files = GetFilesEveryFolder(logger, folder, "*" + extension, SearchOption.AllDirectories, args);
            if (files.Count != 0) filesByExtension.Add(extension, files);
        }
        return filesByExtension;
    }
}
