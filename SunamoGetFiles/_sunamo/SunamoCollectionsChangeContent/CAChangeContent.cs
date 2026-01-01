namespace SunamoGetFiles._sunamo.SunamoCollectionsChangeContent;

/// <summary>
/// Collection change content helper methods
/// </summary>
internal class CAChangeContent
{
    /// <summary>
    /// Removes null or empty values from list based on args settings
    /// </summary>
    /// <param name="args">Optional arguments for controlling removal</param>
    /// <param name="list">List to modify</param>
    private static void RemoveNullOrEmpty(ChangeContentArgsGetFiles? args, List<string> list)
    {
        if (args != null)
        {
            if (args.RemoveNull)
            {
                list.Remove(null);
            }
            if (args.RemoveEmpty)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Trim() == string.Empty)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Changes content of list by applying function to each element.
    /// Directly edits the list in place.
    /// If not every element fulfills pattern, it's good to remove null or default values from result.
    /// The last number in method name indicates the number of parameters passed to the delegate.
    /// </summary>
    /// <param name="args">Optional arguments for post-processing</param>
    /// <param name="list">List to modify</param>
    /// <param name="func">Function to apply to each element</param>
    /// <returns>Modified list</returns>
    internal static List<string> ChangeContent0(ChangeContentArgsGetFiles? args, List<string> list, Func<string, string> func)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = func.Invoke(list[i]);
        }
        RemoveNullOrEmpty(args, list);
        return list;
    }
}
