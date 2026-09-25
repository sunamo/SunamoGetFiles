namespace SunamoGetFiles._public.SunamoArgs;

public class GetFilesEveryFolderArgs
{
    #region Base
    public bool FollowJunctions { get; set; } = false;

    public Func<string, bool>? IsJunctionPoint { get; set; } = null;

    public bool TrimRootFolderAndLeadingBackslashes { get; set; } = false;
    #endregion

    public bool TrimExtension { get; set; } = false;

    public List<string> ExcludeFromLocationsContains { get; set; } = new List<string>();

    public bool DontIncludeNewest { get; set; } = false;

    public Action<List<string>>? ExcludeWithMethod { get; set; } = null;

    public bool ByDateOfLastModifiedAsc { get; set; } = false;

    public Func<string, DateTime?>? LastModifiedFromFileName { get; set; }

    public bool UseMaskFromExtension { get; set; } = false;

    public bool Wildcard { get; set; } = false;

    public Action? Done { get; set; }

    public Action? DoneOnePercent { get; set; }

    public Func<string, bool>? FilterFoundFiles { get; set; }

    public Func<string, bool>? FilterFoundFolders { get; set; }

    public int GetNullIfThereIsMoreThanXFiles { get; set; } = -1;

    public Action<double>? InsertProgressBar { get; set; } = null;

    public Action<double>? InsertProgressBarTime { get; set; } = null;

    public bool ThrowException { get; set; } = false;

    public Action<string>? UpdateTextProgressBar { get; set; } = null;

    public bool UseProgressBar { get; set; } = false;

    public bool UseProgressBarTime { get; set; } = false;

    public List<string> IgnoreFoldersWithName { get; set; } = new();

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs", "dist", "out", ".next"];

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
