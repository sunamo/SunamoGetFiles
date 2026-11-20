namespace SunamoGetFiles._public.SunamoArgs;

/// <summary>
/// Represents the get files every folder args type.
/// </summary>
public class GetFilesEveryFolderArgs
{
    #region Base
    /// <summary>
    /// Gets or sets the follow junctions.
    /// </summary>
    public bool FollowJunctions { get; set; } = false;

    /// <summary>
    /// Gets or sets the is junction point.
    /// </summary>
    public Func<string, bool>? IsJunctionPoint { get; set; } = null;

    /// <summary>
    /// Gets or sets the trim root folder and leading backslashes.
    /// </summary>
    public bool TrimRootFolderAndLeadingBackslashes { get; set; } = false;
    #endregion

    /// <summary>
    /// Gets or sets the trim extension.
    /// </summary>
    public bool TrimExtension { get; set; } = false;

    /// <summary>
    /// Gets or sets the exclude from locations contains.
    /// </summary>
    public List<string> ExcludeFromLocationsContains { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the dont include newest.
    /// </summary>
    public bool DontIncludeNewest { get; set; } = false;

    /// <summary>
    /// Gets or sets the exclude with method.
    /// </summary>
    public Action<List<string>>? ExcludeWithMethod { get; set; } = null;

    /// <summary>
    /// Gets or sets the by date of last modified asc.
    /// </summary>
    public bool ByDateOfLastModifiedAsc { get; set; } = false;

    /// <summary>
    /// Gets or sets the last modified from file name.
    /// </summary>
    public Func<string, DateTime?>? LastModifiedFromFileName { get; set; }

    /// <summary>
    /// Gets or sets the use mask from extension.
    /// </summary>
    public bool UseMaskFromExtension { get; set; } = false;

    /// <summary>
    /// Gets or sets the wildcard.
    /// </summary>
    public bool Wildcard { get; set; } = false;

    /// <summary>
    /// Gets or sets the done.
    /// </summary>
    public Action? Done { get; set; }

    /// <summary>
    /// Gets or sets the done one percent.
    /// </summary>
    public Action? DoneOnePercent { get; set; }

    /// <summary>
    /// Gets or sets the filter found files.
    /// </summary>
    public Func<string, bool>? FilterFoundFiles { get; set; }

    /// <summary>
    /// Gets or sets the filter found folders.
    /// </summary>
    public Func<string, bool>? FilterFoundFolders { get; set; }

    /// <summary>
    /// Gets or sets the get null if there is more than x files.
    /// </summary>
    public int GetNullIfThereIsMoreThanXFiles { get; set; } = -1;

    /// <summary>
    /// Gets or sets the insert progress bar.
    /// </summary>
    public Action<double>? InsertProgressBar { get; set; } = null;

    /// <summary>
    /// Gets or sets the insert progress bar time.
    /// </summary>
    public Action<double>? InsertProgressBarTime { get; set; } = null;

    /// <summary>
    /// Gets or sets the throw exception.
    /// </summary>
    public bool ThrowException { get; set; } = false;

    /// <summary>
    /// Gets or sets the update text progress bar.
    /// </summary>
    public Action<string>? UpdateTextProgressBar { get; set; } = null;

    /// <summary>
    /// Gets or sets the use progress bar.
    /// </summary>
    public bool UseProgressBar { get; set; } = false;

    /// <summary>
    /// Gets or sets the use progress bar time.
    /// </summary>
    public bool UseProgressBarTime { get; set; } = false;

    /// <summary>
    /// Gets or sets the ignore folders with name.
    /// </summary>
    public List<string> IgnoreFoldersWithName { get; set; } = new();

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs", "dist", "out", ".next"];

    /// <summary>
    /// The exclude generated code folders.
    /// </summary>
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
