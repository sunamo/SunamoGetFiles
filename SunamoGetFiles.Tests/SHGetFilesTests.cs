// variables names: ok
namespace SunamoGetFiles.Tests;

using Microsoft.Extensions.Logging;
using SunamoGetFiles._public.SunamoArgs;
#if NET9_0_OR_GREATER
using SunamoTest;
#endif
using System.Text;

/// <summary>
/// Tests for file system get files operations
/// </summary>
public class SHGetFilesTests
{
    /// <summary>
    /// Tests GetFoldersEveryFolder method
    /// </summary>
    [Fact]
    public void GetFoldersEveryFolderTest()
    {
        var files = FSGetFiles.GetFilesEveryFolder(LoggerDummy.Instance, @"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoExceptions\", "*.cs", true, new SunamoGetFiles._public.SunamoArgs.GetFilesEveryFolderArgs { ExcludeGeneratedCodeFolders = true });
    }

    /// <summary>
    /// Tests GetFiles method across all drives
    /// </summary>
    [Fact]
    public void GetFilesTest()
    {
        List<List<string>> results = new List<List<string>>();

        var data = DriveInfo.GetDrives();
        foreach (var item in data)
        {
            results.Add(FSGetFiles.GetFilesEveryFolder(LoggerDummy.Instance, item.RootDirectory.FullName, "*", SearchOption.AllDirectories));
        }

        StringBuilder stringBuilder = new StringBuilder();

        foreach (var item in results)
        {
            stringBuilder.AppendLine(item.Count.ToString());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        File.WriteAllText(@"D:\a.txt", stringBuilder.ToString());
    }

    private ILogger logger =
#if NET9_0_OR_GREATER
        TestLogger.Instance;
#else
        LoggerDummy.Instance;
#endif

    /// <summary>
    /// Tests GetFilesEveryFolder method with various parameters
    /// </summary>
    [Fact]
    public void GetFilesEveryFolderTest()
    {
        var tsx = FSGetFiles.GetFilesEveryFolder(logger, @"C:\Proj_Net\Dealers.BezDodavatele\", "*.ts;*.tsx", SearchOption.AllDirectories,
            new GetFilesEveryFolderArgs() { ExcludeGeneratedCodeFolders = true });
    }
}
