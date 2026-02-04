namespace SunamoGetFiles._public.SunamoArgs;

/// <summary>
/// Arguments for GetFilesEveryFolder operations
/// </summary>
public class GetFilesEveryFolderArgs
{
    #region Base
    /// <summary>
    /// Whether to follow junction points (symbolic links)
    /// </summary>
    public bool FollowJunctions { get; set; } = false;

    /// <summary>
    /// Function to determine if a directory is a junction point
    /// </summary>
    public Func<string, bool>? IsJunctionPoint { get; set; } = null;

    /// <summary>
    /// Whether to trim root folder path and leading backslashes from results
    /// </summary>
    public bool TrimRootFolderAndLeadingBackslashes { get; set; } = false;
    #endregion

    /// <summary>
    /// Whether to trim file extensions from results
    /// </summary>
    public bool TrimExtension { get; set; } = false;

    /// <summary>
    /// List of substrings to exclude from file locations
    /// </summary>
    public List<string> ExcludeFromLocationsContains { get; set; } = new List<string>();

    /// <summary>
    /// Whether to exclude the newest file from results
    /// </summary>
    public bool DontIncludeNewest { get; set; } = false;

    /// <summary>
    /// Custom method to exclude files from results
    /// </summary>
    public Action<List<string>>? ExcludeWithMethod { get; set; } = null;

    /// <summary>
    /// Whether to sort by date of last modification in ascending order
    /// </summary>
    public bool ByDateOfLastModifiedAsc { get; set; } = false;

    /// <summary>
    /// Function to get last modified date from filename
    /// </summary>
    public Func<string, DateTime?>? LastModifiedFromFn { get; set; }

    /// <summary>
    /// Whether to use mask from extension
    /// </summary>
    public bool UseMascFromExtension { get; set; } = false;

    /// <summary>
    /// Whether to use wildcard matching
    /// </summary>
    public bool Wildcard { get; set; } = false;

    /// <summary>
    /// Action to call when operation is done
    /// </summary>
    public Action? Done { get; set; }

    /// <summary>
    /// Action to call when one percent of operation is done
    /// </summary>
    public Action? DoneOnePercent { get; set; }

    /// <summary>
    /// Filter function for found files
    /// </summary>
    public Func<string, bool>? FilterFoundedFiles { get; set; }

    /// <summary>
    /// Filter function for found folders
    /// </summary>
    public Func<string, bool>? FilterFoundedFolders { get; set; }

    /// <summary>
    /// Returns null if there are more than X files found (-1 to disable)
    /// </summary>
    public int GetNullIfThereIsMoreThanXFiles { get; set; } = -1;

    /// <summary>
    /// Action to insert progress bar value
    /// </summary>
    public Action<double>? InsertPb { get; set; } = null;

    /// <summary>
    /// Action to insert progress bar time value
    /// </summary>
    public Action<double>? InsertPbTime { get; set; } = null;

    /// <summary>
    /// Whether to throw exceptions or suppress them
    /// </summary>
    public bool ThrowEx { get; set; } = false;

    /// <summary>
    /// Action to update progress bar text
    /// </summary>
    public Action<string>? UpdateTbPb { get; set; } = null;

    /// <summary>
    /// Whether to use progress bar
    /// </summary>
    public bool UsePb { get; set; } = false;

    /// <summary>
    /// Whether to use progress bar with time
    /// </summary>
    public bool UsePbTime { get; set; } = false;

    /// <summary>
    /// List of folder names to ignore during search
    /// </summary>
    public List<string> IgnoreFoldersWithName { get; set; } = new();

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs", "dist", "out", ".next"];

    /// <summary>
    /// Whether to exclude generated code folders (obj, bin, node_modules, .git, .vs, dist, out, .next)
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