namespace SunamoGetFiles;

partial class FSGetFiles
{
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
