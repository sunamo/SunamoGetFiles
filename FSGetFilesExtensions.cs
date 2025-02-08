namespace SunamoGetFiles;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class FSGetFiles
{
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