namespace SunamoGetFiles._sunamo.SunamoCollectionsChangeContent;

internal class CAChangeContent
{
    private static void removeNullOrEmpty(ChangeContentArgsGetFiles? args, List<string> list)
    {
        if (args != null)
        {
            if (args.RemoveNull)
            {
                list.Remove(null!);
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

    internal static List<string> ChangeContent0(ChangeContentArgsGetFiles? args, List<string> list, Func<string, string> func)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = func.Invoke(list[i]);
        }
        removeNullOrEmpty(args, list);
        return list;
    }
}
