namespace SunamoGetFiles;

/// <summary>
/// Provides methods for getting files from the file system
/// </summary>
public partial class FSGetFiles
{
    /// <summary>
    /// Gets files from every folder
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Root folder to search</param>
    /// <param name="mask">File mask (supports semicolon-separated masks)</param>
    /// <param name="isRecursive">Whether to search recursively</param>
    /// <param name="args">Optional arguments for file search</param>
    /// <returns>List of file paths</returns>
    public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask, bool isRecursive,
        GetFilesEveryFolderArgs? args = null)
    {
        return GetFilesEveryFolder(logger, folder, mask, isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, args);
    }

    /// <summary>
    /// Gets files from every folder with specified search options
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="folder">Root folder to search</param>
    /// <param name="mask">File mask (supports semicolon-separated masks)</param>
    /// <param name="searchOption">Search option (top directory only or all directories)</param>
    /// <param name="args">Optional arguments for file search</param>
    /// <returns>List of file paths</returns>
    public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask = "*", SearchOption searchOption = SearchOption.AllDirectories,
        GetFilesEveryFolderArgs? args = null)
    {
        if (mask.Contains(";"))
        {
            var parts = mask.Split(';');
            List<string> result = new();

            foreach (var item in parts)
            {
                result.AddRange(GetFilesEveryFolder(logger, folder, item, searchOption, args));
            }

            return result;
        }

        if (args == null) args = new GetFilesEveryFolderArgs();

        var list = new List<string>();
        List<string>? directories = null;

        if (args.UsePbTime)
        {
            var message = Translate.FromKey(XlfKeys.Loading) + " " + Translate.FromKey(XlfKeys.FoldersTree) + "...";
            args.InsertPbTime?.Invoke(60);
            args.UpdateTbPb?.Invoke(message);
        }

        directories = new List<string>();
        if (searchOption == SearchOption.AllDirectories)
        {
            FSGetFolders.GetFoldersEveryFolder(logger, directories, folder, "*", new GetFoldersEveryFolderArgs(args));
        }

        if (args.FilterFoundedFolders != null)
        {
            string? currentDirectory = null;
            for (var i = directories.Count - 1; i >= 0; i--)
            {
                currentDirectory = directories[i];
                if (!args.FilterFoundedFolders.Invoke(currentDirectory)) directories.RemoveAt(i);
            }
        }

        if (args.UsePb)
        {
            var message = Translate.FromKey(XlfKeys.Loading) + " " + Translate.FromKey(XlfKeys.FilesTree) + "...";
            args.InsertPb?.Invoke(directories.Count);
            args.UpdateTbPb?.Invoke(message);
        }

        var filesInCurrentDirectory = new List<string>();
        directories.Insert(0, folder);

        foreach (var item in directories)
        {
            try
            {
                var files = Directory.GetFiles(item, mask, SearchOption.TopDirectoryOnly);
                filesInCurrentDirectory.AddRange(files);
                if (args.GetNullIfThereIsMoreThanXFiles != -1)
                    if (filesInCurrentDirectory.Count > args.GetNullIfThereIsMoreThanXFiles)
                    {
                        if (args.UsePb) args.Done?.Invoke();
                        return null!;
                    }
            }
            catch (Exception ex)
            {
                if (args.ThrowEx) ThrowEx.Custom(ex);
            }

            if (args.UsePb) args.DoneOnePercent?.Invoke();

            if (args.FilterFoundedFiles != null)
                for (var i = filesInCurrentDirectory.Count - 1; i >= 0; i--)
                    if (!args.FilterFoundedFiles(filesInCurrentDirectory[i]))
                        filesInCurrentDirectory.RemoveAt(i);

            list.AddRange(filesInCurrentDirectory);
            filesInCurrentDirectory.Clear();
        }

        list = list.Distinct().ToList();
        if (args.UsePb) args.Done?.Invoke();

        for (var i = 0; i < list.Count; i++) list[i] = SH.FirstCharUpper(list[i]);
        if (args.TrimRootFolderAndLeadingBackslashes)
            for (var i = 0; i < list.Count; i++)
                list[i] = list[i].Replace(folder, "").TrimStart('\\').TrimEnd('\\');
        return list;
    }
}