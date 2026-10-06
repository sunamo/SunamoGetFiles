namespace SunamoGetFiles._sunamo.SunamoGetFolders.Args;

internal class GetFoldersEveryFolderArgs
{
    internal bool ThrowException { get; set; } = false;

    internal List<string>? IgnoreFoldersWithName { get; set; } = null;

    internal GetFoldersEveryFolderArgs(GetFilesEveryFolderArgs args)
    {
        ThrowException = args.ThrowException;
        IgnoreFoldersWithName = args.IgnoreFoldersWithName;
    }

    internal GetFoldersEveryFolderArgs()
    {
    }
}
