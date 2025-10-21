// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

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
        //var data = FSGetFiles.GetFilesEveryFolder(logger, @"E:\vs\Projects\_WhenNeedToEditAllCorruptedSlns\CommandsToAllCsFiles.Cmd\", "*.cs", true);

        var f = FSGetFiles.GetFilesEveryFolder(LoggerDummy.Instance, @"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoExceptions\", "*.cs", true, new SunamoGetFiles._public.SunamoArgs.GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });

        //var f = FSGetFiles.GetFilesEveryFolder(logger, @"E:\vs\Projects\", "*.cs", true, new SunamoGetFiles._public.SunamoArgs.GetFilesEveryFolderArgs { IgnoreFoldersWithName = ["obj", "node_modules", ".git", ".vs"], Logger = LoggerDummy.Instance });
    }

    [Fact]
    public void GetFilesTest()
    {
        List<List<string>> r = new List<List<string>>();

        var data = DriveInfo.GetDrives();
        foreach (var item in data)
        {
            r.Add(FSGetFiles.GetFilesEveryFolder(LoggerDummy.Instance, item.RootDirectory.FullName, "*", SearchOption.AllDirectories));
        }

        StringBuilder stringBuilder = new StringBuilder();

        foreach (var item in r)
        {
            //foreach (var item2 in item)
            //{
            //    stringBuilder.AppendLine(item2);
            //}
            stringBuilder.AppendLine(item.Count.ToString());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        File.WriteAllText(@"D:\a.txt", stringBuilder.ToString());
    }

    ILogger logger = TestLogger.Instance;

    [Fact]
    public void GetFilesEveryFolderTest()
    {
        //var data = FSGetFiles.GetFilesEveryFolder(logger, TestLogger.Instance, @"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoThreading\", "Sess.cs", true, new GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });

        //var data = FSGetFiles.GetFilesEveryFolder(logger, @"E:\vs\Projects\sunamo.net\Clients\src", "*.js;*.cjs", true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, new GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });

        //const string pnusSrc = @"C:\Proj_Net\usys-siesta\src\";
        //var f = FSGetFiles.GetFilesEveryFolder(logger, pnusSrc, "*.ts;*.tsx", true, new() { ExcludeGeneratedCodeFolders = true });

        //
        var tsx = FSGetFiles.GetFilesEveryFolder(logger, @"C:\Proj_Net\Dealers.BezDodavatele\", "*.ts;*.tsx", SearchOption.AllDirectories,
            new GetFilesEveryFolderArgs() { ExcludeGeneratedCodeFolders = true });
    }
}