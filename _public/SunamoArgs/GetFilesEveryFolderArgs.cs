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
    public List<string> IgnoreFoldersWithName { get; set; }
}