namespace SunamoGetFiles;

internal class FSFilesCommentedEveryFolder
{
    //    /// <summary>
    //    ///     When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    //    ///     A1 have to be with ending backslash
    //    ///     A4 must have underscore otherwise is suggested while I try type true
    //    ///     A2 can be delimited by semicolon. In case more extension use GetFilesOfExtensions
    //    /// </summary>
    //    /// <param name="folder"></param>
    //    /// <param name="mask"></param>
    //    /// <param name="searchOption"></param>
    //    public static List<string> GetFiles(ILogger logger, string folder2, string mask, SearchOption searchOption, GetFilesArgs a = null)
    //    {


    //#if DEBUG
    //        if (folder2.TrimEnd('\\') == @"\monoConsoleSqlClient")
    //        {
    //        }
    //#endif

    //        if (!Directory.Exists(folder2) && !folder2.Contains(";"))
    //            //ThisApp.Warning(folder2 + "does not exists");
    //            return new List<string>();
    //        if (a == null) a = new GetFilesArgs();
    //        var folders = SHSplit.Split(folder2, ";");
    //        //if (CA.PostfixIfNotEnding != null)
    //        //{
    //        //    CA.PostfixIfNotEnding("\"", folders);
    //        //}
    //        for (var i = 0; i < folders.Count; i++) folders[i] = folders[i].TrimEnd('\\') + "\\";
    //        var list = new List<string>();
    //        foreach (var folder in folders)
    //            if (!Directory.Exists(folder))
    //            {
    //            }
    //            else
    //            {
    //                //Task.Run<>(async () => await FunctionAsync());
    //                //var r = Task.Run<List<string>>(async () => await GetFilesMoreMascAsync(folder, mask, searchOption));
    //                //return r.Result;
    //                var l2 = GetFilesEveryFolder(logger, folder, mask, searchOption,
    //                    new() { followJunctions = a.followJunctions });
    //                list.AddRange(l2);

    //                #region Commented

    //                //if (mask.Contains(";"))
    //                //{
    //                //    //list = new List<string>();
    //                //    var masces = SHSplit.Split(mask, ";");
    //                //    foreach (var item in masces)
    //                //    {
    //                //        var masc = item;
    //                //        if (getFilesArgs.useMascFromExtension)
    //                //        {
    //                //            masc = FS.MascFromExtension(item);
    //                //        }
    //                //        try
    //                //        {
    //                //            list.AddRange(GetFilesMoreMasc(folder, masc, searchOption));
    //                //        }
    //                //        catch (Exception ex)
    //                //        {
    //                //        }
    //                //    }
    //                //}
    //                //else
    //                //{
    //                //    try
    //                //    {
    //                //        var folder3 = FS.WithoutEndSlash(folder);
    //                //        DirectoryInfo di = new DirectoryInfo(folder3);
    //                //        var masc = mask;
    //                //        if (getFilesArgs.useMascFromExtension)
    //                //        {
    //                //            masc = FS.MascFromExtension(mask);
    //                //        }
    //                //        var files = di.GetFiles(masc, searchOption);
    //                //        var files2 = files.Select(d => d.FullName);
    //                //        //list.AddRange(GetFiles(folder3, masc, searchOption));
    //                //        list.AddRange(files2);
    //                //    }
    //                //    catch (Exception ex)
    //                //    {
    //                //    }
    //                //}

    //                #endregion
    //            }

    //        FilterByGetFilesArgs(list, folders, a);
    //        return list;
    //    }

    //public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
    //{
    //    try
    //    {
    //        return DirectoryMs.GetFiles(folder, mask, searchOption).ToList();
    //    }
    //    catch (Exception ex)
    //    {
    //        // todo zapsat do logu nebo vrátit seznam - chyba přístupu, neexistuje, ...
    //        logger.LogError(Exceptions.TextOfExceptions(ex));
    //    }

    //    return new List<string>();
    //}
}