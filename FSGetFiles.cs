namespace SunamoGetFiles;

public partial class FSGetFiles
{
    public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask, bool rek,
        GetFilesEveryFolderArgs? e = null)
    {
        return GetFilesEveryFolder(logger, folder, mask, rek ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, e);
    }

    /// <summary>
    ///     In item1 is all directories, in Item2 all files
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="ask"></param>
    /// <param name="searchOption"></param>
    /// <param name="_trimA1"></param>
    public static List<string> GetFilesEveryFolder(ILogger logger, string folder, string mask = "*", SearchOption searchOption = SearchOption.AllDirectories,
        GetFilesEveryFolderArgs? e = null)
    {
        if (mask.Contains(";"))
        {
            var parts = mask.Split(';');
            List<string> result = new();

            foreach (var item in parts)
            {
                result.AddRange(GetFilesEveryFolder(logger, folder, item, searchOption, e));
            }

            return result;
        }

        if (mask.Contains(";"))
        {
            var masces = SHSplit.Split(mask, ";");

            List<string> result = new List<string>();

            foreach (var item in masces)
            {
                result.AddRange(GetFilesEveryFolder(logger, folder, item, searchOption, e));
            }

            return result;
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
        if (searchOption == SearchOption.AllDirectories)
        {
            FSGetFolders.GetFoldersEveryFolder(logger, dirs, folder, "*", new GetFoldersEveryFolderArgs(e));
        }
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


                //d.Clear();
                var f = Directory.GetFiles(item, mask, SearchOption.TopDirectoryOnly);
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
                list[i] = list[i].Replace(folder, "").TrimStart('\\').TrimEnd('\\');
        return list;
    }
}