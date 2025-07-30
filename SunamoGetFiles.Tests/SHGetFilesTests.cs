namespace SunamoGetFiles.Tests;

using Microsoft.Extensions.Logging;
using SunamoGetFiles._public.SunamoArgs;
using SunamoTest;
using System.Text;

public class SHGetFilesTests
{
    //ILogger logger = LoggerDummy.Instance;

    public void GetFoldersEveryFolderTest()
    {
        //var d = FSGetFiles.GetFilesEveryFolder(logger, @"E:\vs\Projects\_WhenNeedToEditAllCorruptedSlns\CommandsToAllCsFiles.Cmd\", "*.cs", true);

        var f = FSGetFiles.GetFilesEveryFolder(LoggerDummy.Instance, @"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoExceptions\", "*.cs", true, new SunamoGetFiles._public.SunamoArgs.GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });

        //var f = FSGetFiles.GetFilesEveryFolder(logger, @"E:\vs\Projects\", "*.cs", true, new SunamoGetFiles._public.SunamoArgs.GetFilesEveryFolderArgs { IgnoreFoldersWithName = ["obj", "node_modules", ".git", ".vs"], Logger = LoggerDummy.Instance });
    }

    [Fact]
    public void GetFilesTest()
    {
        List<List<string>> r = new List<List<string>>();

        var d = DriveInfo.GetDrives();
        foreach (var item in d)
        {
            r.Add(FSGetFiles.GetFilesEveryFolder(LoggerDummy.Instance, item.RootDirectory.FullName, "*", SearchOption.AllDirectories));
        }

        StringBuilder sb = new StringBuilder();

        foreach (var item in r)
        {
            //foreach (var item2 in item)
            //{
            //    sb.AppendLine(item2);
            //}
            sb.AppendLine(item.Count.ToString());
            sb.AppendLine();
            sb.AppendLine();
        }

        File.WriteAllText(@"D:\a.txt", sb.ToString());
    }

    ILogger logger = TestLogger.Instance;

    [Fact]
    public void GetFilesEveryFolderTest()
    {
        //var d = FSGetFiles.GetFilesEveryFolder(logger, TestLogger.Instance, @"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoThreading\", "Sess.cs", true, new GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });

        //var d = FSGetFiles.GetFilesEveryFolder(logger, @"E:\vs\Projects\sunamo.net\Clients\src", "*.js;*.cjs", true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, new GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });

        //const string pnusSrc = @"C:\Proj_Net\usys-siesta\src\";
        //var f = FSGetFiles.GetFilesEveryFolder(logger, pnusSrc, "*.ts;*.tsx", true, new() { ExcludeGeneratedCodeFolders = true });

        //
        var tsx = FSGetFiles.GetFilesEveryFolder(logger, @"C:\Proj_Net\Dealers.BezDodavatele\", "*.ts;*.tsx", SearchOption.AllDirectories,
            new GetFilesEveryFolderArgs() { ExcludeGeneratedCodeFolders = true });
    }
}