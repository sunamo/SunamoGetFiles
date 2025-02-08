namespace SunamoGetFiles;
internal class FSGetFilesCommented
{
    //public static List<string> GetFilesWithoutNodeModules(ILogger logger, string item, string masc, bool? rec, GetFilesArgs a = null)
    //{
    //    if (a == null) a = new GetFilesArgs();
    //    a.excludeFromLocationsCOntains.Add("de_mo");
    //    a.excludeFromLocationsCOntains = a.excludeFromLocationsCOntains.Distinct().ToList();
    //    return GetFiles(logger, item, masc, rec, a);
    //}

    //public static List<string> GetFiles(ILogger logger, string folderPath, string masc, bool? rec, GetFilesArgs a = null)
    //{
    //    var so = SearchOption.TopDirectoryOnly;
    //    var b = rec.Value;
    //    if (b) so = SearchOption.AllDirectories;
    //    return
    //        GetFiles(logger, folderPath, masc, so, a);
    //}

    //public static List<string> GetFiles(ILogger logger, string folderPath, string masc)
    //{
    //    return GetFiles(logger, folderPath, masc, SearchOption.TopDirectoryOnly);
    //}



    //    public static List<string> GetFilesMoreMasc(ILogger logger, string path, string masc, SearchOption searchOption,
    //        GetFilesMoreMascArgs e = null)
    //    {
    //        if (e == null) e = new GetFilesMoreMascArgs();
    //#if DEBUG
    //        string d = null;
    //        if (e.LoadFromFileWhenDebug)
    //        {
    //            var s = FS.ReplaceInvalidFileNameChars(string.Join(path, masc, searchOption));
    //            //d = AppData.ci.GetFile(AppFolders.Cache, "GetFilesMoreMasc" + s + ".txt");
    //            //if (File.Exists(d))
    //            //{
    //            //    return File.ReadAllText(path).ToList();
    //            //}
    //        }
    //#endif
    //        var c = ",";
    //        var sc = ";";
    //        var result = new List<string>();
    //        var masks = new List<string>();
    //        if (masc.Contains(c))
    //            masks.AddRange(SHSplit.SplitMore(masc, c));
    //        else if (masc.Contains(sc))
    //            masks.AddRange(SHSplit.SplitMore(masc, sc));
    //        else
    //            masks.Add(masc);

    //        #region Added 27-8-23

    //        //if (searchOption == SearchOption.AllDirectories)
    //        //{
    //        //    foreach (var item in masks)
    //        //    {
    //        //        result.AddRange(GetFiles(path, item, SearchOption.TopDirectoryOnly));
    //        //    }
    //        //}

    //        #endregion

    //        if (e.deleteFromDriveWhenCannotBeResolved)
    //            foreach (var item2 in masks)
    //            {
    //                //if(SHSH.ContainsOnlyCase())
    //                var item = FS.AllIncludeIfOnlyLetters(item2);
    //                try
    //                {
    //                    result.AddRange(GetFiles(logger, path, item, searchOption));
    //                }
    //                catch (Exception ex)
    //                {
    //                    if (ex.Message.StartsWith(NetFxExceptionsNotTranslateAble
    //                            .TheNameOfTheFileCannotBeResolvedByTheSystem))
    //                    {
    //                        // Nesmysl, celou dobu musím vědět s čím pracuji
    //                        //FS.TryDeleteDirectoryOrFile(path);
    //                    }

    //                    ThrowEx.Custom(ex);
    //                }
    //            }
    //        else
    //            foreach (var item2 in masks)
    //            {
    //                var item = FS.AllIncludeIfOnlyLetters(item2);
    //                result.AddRange(GetFiles(logger, path, item, searchOption));
    //            }

    //        if (result.Count > 0) result[0] = SH.FirstCharUpper(result[0]);
    //        CAChangeContent.ChangeContent0(null, result, SH.FirstCharUpper);
    //#if DEBUG
    //        if (e.LoadFromFileWhenDebug)
    //            if (File.Exists(d))
    //                File.WriteAllLinesAsync(d, result);
    //#endif
    //        return result;
    //    }
}
