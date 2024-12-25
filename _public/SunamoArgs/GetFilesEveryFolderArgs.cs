namespace SunamoGetFiles._public.SunamoArgs;

public class GetFilesEveryFolderArgs : GetFilesBaseArgs
{
    public Action Done;
    public Action DoneOnePercent;
    public Func<string, bool> FilterFoundedFiles;
    public Func<string, bool> FilterFoundedFolders;
    public int getNullIfThereIsMoreThanXFiles = -1;
    public Action<double> InsertPb = null;
    public Action<double> InsertPbTime = null;
    public bool throwEx = false;
    public Action<string> UpdateTbPb = null;
    public bool usePb = false;
    public bool usePbTime = false;
    public List<string> IgnoreFoldersWithName { get; set; } = new();

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs"];
    public bool ExcludeGeneratedCodeFolders
    {
        set
        {
            if (value)
            {
                foreach (var item in codeFolders)
                {
                    if (!IgnoreFoldersWithName.Contains(item))
                    {
                        IgnoreFoldersWithName.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in codeFolders)
                {
                    IgnoreFoldersWithName.Remove(item);
                }
            }
        }
    }
}