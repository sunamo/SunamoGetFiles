namespace SunamoGetFiles;

using DirectoryMs = Directory;

public class FSGetFiles
{
    public static List<string> GetFiles(ILogger logger, string folder, string mask, SearchOption searchOption)
    {
        try
        {
            return DirectoryMs.GetFiles(folder, mask, searchOption).ToList();
        }
        catch (Exception ex)
        {
            // todo zapsat do logu nebo vrátit seznam - chyba přístupu, neexistuje, ...
            logger.LogError(Exceptions.TextOfExceptions(ex));
        }

        return new List<string>();
    }



    public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask, bool rek,
        GetFilesEveryFolderArgs e = null)
    {
        return GetFilesEveryFolder(logger, folder, mask, rek ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, e);
    }

    /// <summary>
    ///     When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    ///     A1 have to be with ending backslash
    ///     A4 must have underscore otherwise is suggested while I try type true
    ///     A2 can be delimited by semicolon. In case more extension use GetFilesOfExtensions
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    /// <param name="searchOption"></param>
    public static async Task<List<string>> GetFilesAsync(ILogger logger, string folder2, string mask, SearchOption searchOption,
        GetFilesArgs getFilesArgs = null)
    {
        if (!Directory.Exists(folder2) && !folder2.Contains(";"))
            //ThisApp.Warning(folder2 + "does not exists");
            return new List<string>();
        if (getFilesArgs == null) getFilesArgs = new GetFilesArgs();
        var folders = SHSplit.SplitMore(folder2, ";");
        for (var i = 0; i < folders.Count; i++) folders[i] = folders[i].TrimEnd('\\') + "\"";
        //CA.PostfixIfNotEnding("\"", folders);
        var list = new List<string>();
        foreach (var folder in folders)
            if (!Directory.Exists(folder))
            {
            }
            else
            {
                return GetFilesMoreMasc(logger, folder, mask, searchOption);
            }

        //CAChangeContent.ChangeContent0(null, list, d => SH.FirstCharUpper(d));
        for (var i = 0; i < list.Count; i++) list[i] = SH.FirstCharUpper(list[i]);
        if (getFilesArgs._trimA1AndLeadingBs)
            foreach (var folder in folders)
                //list = CAChangeContent.ChangeContent0(null, list, d => d = d.Replace(folder, ""));
                for (var i = 0; i < list.Count; i++)
                    list[i] = list[i].Replace(folder, "");
        if (getFilesArgs._trimA1AndLeadingBs)
            foreach (var folder in folders)
            {
                list = CAChangeContent.ChangeContent0(null, list, d => d = SHParts.RemoveAfterLast(d, '.'));
                for (var i = 0; i < list.Count; i++) list[i] = SHParts.RemoveAfterLast(list[i], '.');
            }

        if (getFilesArgs.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in getFilesArgs.excludeFromLocationsCOntains)
                list = list.Where(d => !d.Contains(item)).ToList();
        //CA.RemoveWhichContains(list, item, false);
        Dictionary<string, DateTime> dictLastModified = null;
        var isLastModifiedFromFn = getFilesArgs.LastModifiedFromFn != null;
        if (getFilesArgs.dontIncludeNewest || getFilesArgs.byDateOfLastModifiedAsc || isLastModifiedFromFn)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? dt = null;
                if (isLastModifiedFromFn) dt = getFilesArgs.LastModifiedFromFn(Path.GetFileNameWithoutExtension(item));
                if (!dt.HasValue) dt = FS.LastModified(item);
                dictLastModified.Add(item, dt.Value);
            }

            list = dictLastModified.OrderBy(t => t.Value).Select(r => r.Key).ToList();
        }

        if (getFilesArgs.dontIncludeNewest) list.RemoveAt(list.Count - 1);
        if (getFilesArgs.excludeWithMethod != null) getFilesArgs.excludeWithMethod.Invoke(list);
        return list;
    }

    /// <summary>
    ///     In item1 is all directories, in Item2 all files
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="ask"></param>
    /// <param name="searchOption"></param>
    /// <param name="_trimA1"></param>
    public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask, SearchOption searchOption,
        GetFilesEveryFolderArgs e = null)
    {
        if (mask.Contains(";"))
        {
            ThrowEx.Custom("More extensions is not supported by .NET! Use FilesOfExtensions() instead!");
        }

#if DEBUG
        if (folder == @"D:\_Test\EveryLine\EveryLine\SearchCodeElementsUC\")
        {
        }
#endif
        if (e == null) e = new GetFilesEveryFolderArgs();
        // TODO: některé soubory vrací vícekrát. toto je workaround než zjistím proč
        // TODO: je důležité se toho zbavit co nejdříve protože při načítání to zbytečně zpomaluje
        var list = new List<string>();
        List<string> dirs = null;
        var measureTime = false;
        //if (measureTime)
        //{
        //    //StopwatchStatic.Start();
        //}
        // There is not exc handle needed, its slowly then
        //try
        //{
        if (e.usePbTime)
        {
            var m = Translate.FromKey(XlfKeys.Loading) + " " + Translate.FromKey(XlfKeys.FoldersTree) + "...";
            e.InsertPbTime(60);
            e.UpdateTbPb(m);
        }

        dirs = new List<string>();
        FSGetFolders.GetFoldersEveryFolder(logger, dirs, folder, "*", new GetFoldersEveryFolderArgs(e));
#if DEBUG
        //int before = dirs.Count;
#endif
        if (e.FilterFoundedFolders != null)
        {
            string si = null;
            for (var i = dirs.Count - 1; i >= 0; i--)
            {
                si = dirs[i];
                //if (si.Contains(@"\standard\.git"))
                //{
                //}
                if (!e.FilterFoundedFolders.Invoke(si)) dirs.RemoveAt(i);
            }
        }
#if DEBUG
        //int after = dirs.Count;
#endif

        #region MyRegion

        //ClipboardHelper.SetLines(dirs);
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception(Translate.FromKey(XlfKeys.GetFilesWithPath)+": " + folder);
        //}

        #endregion

        //if (measureTime)
        //{
        //    StopwatchStatic.StopAndPrintElapsed("GetFoldersEveryFolder");
        //}
        //if (measureTime)
        //{
        //    StopwatchStatic.Start();
        //}
        if (e.usePb)
        {
            var m = Translate.FromKey(XlfKeys.Loading) + " " + Translate.FromKey(XlfKeys.FilesTree) + "...";
            e.InsertPb(dirs.Count);
            e.UpdateTbPb(m);
        }

        var d = new List<string>();
        //Není třeba, již volám dole e.Done(); / e.DonePartially();
        //IProgressBarHelper pbh = null;
        //if (e.progressBarHelper != null)
        //{
        //    pbh = e.progressBarHelper.CreateInstance(e.pb, dirs.Count, e.pb);
        //}
        dirs.Insert(0, folder);

        foreach (var item in dirs)
        {
            try
            {
#if DEBUG
                if (item == @"E:\vs\Projects\sunamo.net\Clients\src\packages\vue-shared")
                {

                }
#endif

                //d.Clear();
                var f = GetFiles(logger, item, mask, SearchOption.TopDirectoryOnly);
                d.AddRange(f);
                if (e.getNullIfThereIsMoreThanXFiles != -1)
                    if (d.Count > e.getNullIfThereIsMoreThanXFiles)
                    {
                        if (e.usePb) e.Done();
                        return null;
                    }
            }
            catch (Exception ex)
            {
                if (e.throwEx) ThrowEx.Custom(ex);

                // Not throw exception, it's probably Access denied on Documents and Settings etc
                //ThrowEx.FileSystemException( ex);
            }

#if DEBUG
            if (d.Any())
            {

            }
#endif


            if (e.usePb) e.DoneOnePercent();
#if DEBUG
            //before = d.Count;
#endif
            if (e.FilterFoundedFiles != null)
                for (var i = d.Count - 1; i >= 0; i--)
                    if (!e.FilterFoundedFiles(d[i]))
                        d.RemoveAt(i);
#if DEBUG
            //after = d.Count;
            //if (before != 0 && after == 0)
            //{
            //}
#endif
            list.AddRange(d);
            d.Clear();
        }

        list = list.Distinct().ToList();
        if (e.usePb) e.Done();
        //if (measureTime)
        //{
        //    StopwatchStatic.StopAndPrintElapsed("GetFiles");
        //}
        //CAChangeContent.ChangeContent0(null, list, d2 => SH.FirstCharUpper(d2));
        for (var i = 0; i < list.Count; i++) list[i] = SH.FirstCharUpper(list[i]);
        if (e._trimA1AndLeadingBs)
            //list = CAChangeContent.ChangeContent0(null, list, d3 => d3 = d3.Replace(folder, "").TrimStart('\\'));
            for (var i = 0; i < list.Count; i++)
                list[i] = list[i].Replace(folder, "").TrimStart('\\');
        return list;
    }

    public static List<string> GetFilesWithoutArgs(ILogger logger, string folderPath, string masc, bool? rec)
    {
        return GetFiles(logger, folderPath, masc, rec);
    }

    public static List<string> GetFiles(ILogger logger, string folderPath, string masc, bool? rec, GetFilesArgs a = null)
    {
        var so = SearchOption.TopDirectoryOnly;
        var b = rec.Value;
        if (b) so = SearchOption.AllDirectories;
        return
            GetFiles(logger, folderPath, masc, so, a);
    }

    public static List<string> GetFiles(ILogger logger, string folderPath, string masc)
    {
        return GetFiles(logger, folderPath, masc, SearchOption.TopDirectoryOnly);
    }

    public static
#if ASYNC
        async Task<Dictionary<string, string>>
#else
Dictionary<string, string>
#endif
        GetFilesWithContentInDictionary(ILogger logger, string item, string v, SearchOption allDirectories)
    {
        var r = new Dictionary<string, string>();
        var f = GetFiles(logger, item, v, allDirectories);
        foreach (var item2 in f)
            r.Add(item2,
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item2));
        return r;
    }

    /// <summary>
    ///     C:\Users\w\AppData\Roaming\sunamo\
    /// </summary>
    /// <param name="item2"></param>
    /// <param name="exts"></param>
    public static List<string> GetFilesOfExtensions(ILogger logger, string item2, SearchOption so, params string[] exts)
    {
        var vr = new List<string>();
        foreach (var item in exts) vr.AddRange(GetFiles(logger, item2, "*" + item, so));
        return vr;
    }

    /// <summary>
    ///     When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    ///     A1 have to be with ending backslash
    ///     A4 must have underscore otherwise is suggested while I try type true
    ///     A2 can be delimited by semicolon. In case more extension use GetFilesOfExtensions
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    /// <param name="searchOption"></param>
    public static List<string> GetFiles(ILogger logger, string folder2, string mask, SearchOption searchOption, GetFilesArgs a = null)
    {
#if DEBUG
        if (folder2.TrimEnd('\\') == @"\monoConsoleSqlClient")
        {
        }
#endif
        if (!Directory.Exists(folder2) && !folder2.Contains(";"))
            //ThisApp.Warning(folder2 + "does not exists");
            return new List<string>();
        if (a == null) a = new GetFilesArgs();
        var folders = SHSplit.SplitMore(folder2, ";");
        //if (CA.PostfixIfNotEnding != null)
        //{
        //    CA.PostfixIfNotEnding("\"", folders);
        //}
        for (var i = 0; i < folders.Count; i++) folders[i] = folders[i].TrimEnd('\\') + "\\";
        var list = new List<string>();
        foreach (var folder in folders)
            if (!Directory.Exists(folder))
            {
            }
            else
            {
                //Task.Run<>(async () => await FunctionAsync());
                //var r = Task.Run<List<string>>(async () => await GetFilesMoreMascAsync(folder, mask, searchOption));
                //return r.Result;
                var l2 = GetFilesMoreMasc(logger, folder, mask, searchOption,
                    new GetFilesMoreMascArgs { followJunctions = a.followJunctions });
                list.AddRange(l2);

                #region Commented

                //if (mask.Contains(";"))
                //{
                //    //list = new List<string>();
                //    var masces = SHSplit.SplitMore(mask, ";");
                //    foreach (var item in masces)
                //    {
                //        var masc = item;
                //        if (getFilesArgs.useMascFromExtension)
                //        {
                //            masc = FS.MascFromExtension(item);
                //        }
                //        try
                //        {
                //            list.AddRange(GetFilesMoreMasc(folder, masc, searchOption));
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //}
                //else
                //{
                //    try
                //    {
                //        var folder3 = FS.WithoutEndSlash(folder);
                //        DirectoryInfo di = new DirectoryInfo(folder3);
                //        var masc = mask;
                //        if (getFilesArgs.useMascFromExtension)
                //        {
                //            masc = FS.MascFromExtension(mask);
                //        }
                //        var files = di.GetFiles(masc, searchOption);
                //        var files2 = files.Select(d => d.FullName);
                //        //list.AddRange(GetFiles(folder3, masc, searchOption));
                //        list.AddRange(files2);
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}

                #endregion
            }

        FilterByGetFilesArgs(list, folders, a);
        return list;
    }

    public static List<long> GetFilesSizes(ILogger logger, List<string> f)
    {
        var sizes = new List<long>();
        foreach (var item in f)
        {
            try
            {
                sizes.Add(new FileInfo(item).Length);
            }
            catch (Exception ex)
            {
                logger.LogError(Exceptions.TextOfExceptions(ex));
            }

        }
        return sizes;
    }

    public static List<string> GetFilesMoreMasc(ILogger logger, string path, string masc, SearchOption searchOption,
        GetFilesMoreMascArgs e = null)
    {
        if (e == null) e = new GetFilesMoreMascArgs();
#if DEBUG
        string d = null;
        if (e.LoadFromFileWhenDebug)
        {
            var s = FS.ReplaceInvalidFileNameChars(string.Join(path, masc, searchOption));
            //d = AppData.ci.GetFile(AppFolders.Cache, "GetFilesMoreMasc" + s + ".txt");
            //if (File.Exists(d))
            //{
            //    return File.ReadAllText(path).ToList();
            //}
        }
#endif
        var c = ",";
        var sc = ";";
        var result = new List<string>();
        var masks = new List<string>();
        if (masc.Contains(c))
            masks.AddRange(SHSplit.SplitMore(masc, c));
        else if (masc.Contains(sc))
            masks.AddRange(SHSplit.SplitMore(masc, sc));
        else
            masks.Add(masc);

        #region Added 27-8-23

        //if (searchOption == SearchOption.AllDirectories)
        //{
        //    foreach (var item in masks)
        //    {
        //        result.AddRange(GetFiles(path, item, SearchOption.TopDirectoryOnly));
        //    }
        //}

        #endregion

        if (e.deleteFromDriveWhenCannotBeResolved)
            foreach (var item2 in masks)
            {
                //if(SHSH.ContainsOnlyCase())
                var item = FS.AllIncludeIfOnlyLetters(item2);
                try
                {
                    result.AddRange(GetFiles(logger, path, item, searchOption));
                }
                catch (Exception ex)
                {
                    if (ex.Message.StartsWith(NetFxExceptionsNotTranslateAble
                            .TheNameOfTheFileCannotBeResolvedByTheSystem))
                    {
                        // Nesmysl, celou dobu musím vědět s čím pracuji
                        //FS.TryDeleteDirectoryOrFile(path);
                    }

                    ThrowEx.Custom(ex);
                }
            }
        else
            foreach (var item2 in masks)
            {
                var item = FS.AllIncludeIfOnlyLetters(item2);
                result.AddRange(GetFiles(logger, path, item, searchOption));
            }

        if (result.Count > 0) result[0] = SH.FirstCharUpper(result[0]);
        CAChangeContent.ChangeContent0(null, result, SH.FirstCharUpper);
#if DEBUG
        if (e.LoadFromFileWhenDebug)
            if (File.Exists(d))
                File.WriteAllLinesAsync(d, result);
#endif
        return result;
    }

    public static List<string> GetFiles(ILogger logger, string p, bool rek)
    {
        return Directory.GetFiles(p, "*.*", rek ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
    }

    public static void FilterByGetFilesArgs(List<string> list, IEnumerable<string> folders, GetFilesArgs a)
    {
        if (a == null) a = new GetFilesArgs();
        CAChangeContent.ChangeContent0(null, list, d => SH.FirstCharUpper(d));
        if (a._trimA1AndLeadingBs)
            foreach (var folder in folders)
                list = CAChangeContent.ChangeContent0(null, list, d => d = d.Replace(folder, "").TrimEnd('\\'));
        if (a._trimExt)
            foreach (var folder in folders)
                list = CAChangeContent.ChangeContent0(null, list, d => d = SHParts.RemoveAfterLast(d, '.'));
        if (a.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in a.excludeFromLocationsCOntains)
                list = list.Where(d => !d.Contains(item)).ToList();
        //CA.RemoveWhichContains(list, item, false);
        Dictionary<string, DateTime> dictLastModified = null;
        var isLastModifiedFromFn = a.LastModifiedFromFn != null;
        if (a.dontIncludeNewest || a.byDateOfLastModifiedAsc || isLastModifiedFromFn)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? dt = null;
                if (isLastModifiedFromFn) dt = a.LastModifiedFromFn(Path.GetFileNameWithoutExtension(item));
                if (!dt.HasValue) dt = FS.LastModified(item);
                dictLastModified.Add(item, dt.Value);
            }

            list = dictLastModified.OrderBy(t => t.Value).Select(r => r.Key).ToList();
        }

        if (a.dontIncludeNewest) list.RemoveAt(list.Count - 1);
        if (a.excludeWithMethod != null) a.excludeWithMethod.Invoke(list);
    }

    /// <summary>
    ///     No recursive, all extension
    /// </summary>
    /// <param name="path"></param>
    public static List<string> GetFiles(ILogger logger, string path)
    {
        return GetFiles(logger, path, "*", SearchOption.TopDirectoryOnly);
    }

    public static string GetFilesSize(List<string> winrarFiles, ComputerSizeUnitsGetFiles s)
    {
        long size = 0;
        foreach (var item in winrarFiles)
        {
            var fi = new FileInfo(item);
            size += fi.Length;
        }

        return FS.GetSizeInAutoString(size);
    }

    public static List<string> AllFilesInFolders(ILogger logger, IList<string> folders, IList<string> exts, SearchOption so,
        GetFilesArgs a = null)
    {
        var files = new List<string>();
        foreach (var item in folders)
            foreach (var ext in exts)
                files.AddRange(GetFiles(logger, item, FS.MascFromExtension(ext), so, a));
        return files;
    }

    public static List<string> GetFilesWithoutNodeModules(ILogger logger, string item, string masc, bool? rec, GetFilesArgs a = null)
    {
        if (a == null) a = new GetFilesArgs();
        a.excludeFromLocationsCOntains.Add("de_mo");
        a.excludeFromLocationsCOntains = a.excludeFromLocationsCOntains.Distinct().ToList();
        return GetFiles(logger, item, masc, rec, a);
    }

    public static
#if ASYNC
        async Task<List<string>>
#else
List<string>
#endif
        FilesWhichContainsAll(ILogger logger, object sunamo, string masc, IList<string> mustContains)
    {
        var mcl = mustContains.Count();
        var ls = new List<string>();
        IList<string> f = null;
        if (sunamo is IList<string>)
            f = (IList<string>)sunamo;
        else
            f = GetFiles(logger, sunamo.ToString(), masc, true);
        foreach (var item in f)
        {
            var c =
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item);
            if (mustContains.Where(d => c.Contains(d)).Count() ==
                mcl) /*CASHSH.ContainsAnyFromElement(c, mustContains).Count == mcl*/ ls.Add(item);
        }

        return ls;
    }

    public static FileInfo[] GetFileInfosOfExtensions(string item2, SearchOption so, params string[] exts)
    {
        var vr = new List<FileInfo>();
        var di = new DirectoryInfo(item2);
        foreach (var item in exts) vr.AddRange(di.GetFiles("*" + item, so));
        return vr.ToArray();
    }

    /// <summary>
    ///     Non recursive
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="fileExt"></param>
    public static List<string> FilesOfExtension(ILogger logger, string folder, string fileExt)
    {
        return GetFiles(logger, folder, "*." + fileExt, SearchOption.TopDirectoryOnly);
    }

    public static List<string> FilesOfExtensionsArray(string folder, List<string> extension)
    {
        var foundedFiles = new List<string>();
        FS.NormalizeExtensions(extension);
        var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            var ext = FS.GetNormalizedExtension(item);
            if (extension.Contains(ext)) foundedFiles.Add(ext);
        }

        return foundedFiles;
    }

    /// <summary>
    ///     Keys returns with normalized ext
    ///     In case zero files of ext wont be included in dict
    /// </summary>
    /// <param name="folderFrom"></param>
    /// <param name="extensionsWithDot"></param>
    public static Dictionary<string, List<string>> FilesOfExtensions(ILogger logger, string folderFrom, GetFilesEveryFolderArgs a, params string[] extensionsWithDot)
    {
        var dict = new Dictionary<string, List<string>>();
        foreach (var item in extensionsWithDot)
        {
            var ext = FS.NormalizeExtension(item);
            var files = GetFilesEveryFolder(logger, folderFrom, "*" + ext, SearchOption.AllDirectories, a);
            if (files.Count != 0) dict.Add(ext, files);
        }

        return dict;
    }
}