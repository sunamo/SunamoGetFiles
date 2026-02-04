namespace SunamoGetFiles._sunamo.SunamoGetFolders.Args;

/// <summary>
/// Arguments for GetFoldersEveryFolder operations
/// </summary>
internal class GetFoldersEveryFolderArgs
{
    /// <summary>
    /// Whether to throw exceptions or suppress them
    /// </summary>
    internal bool ThrowEx { get; set; } = false;

    /// <summary>
    /// List of folder names to ignore during search
    /// </summary>
    internal List<string>? IgnoreFoldersWithName { get; set; } = null;

    /// <summary>
    /// Initializes from GetFilesEveryFolderArgs
    /// </summary>
    /// <param name="args">Source arguments</param>
    internal GetFoldersEveryFolderArgs(GetFilesEveryFolderArgs args)
    {
        ThrowEx = args.ThrowEx;
        IgnoreFoldersWithName = args.IgnoreFoldersWithName;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    internal GetFoldersEveryFolderArgs()
    {
    }
}