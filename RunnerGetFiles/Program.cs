namespace RunnerGetFiles;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SunamoCl.SunamoCmd;
using SunamoGetFiles.Tests;

internal class Program
{
    static ServiceCollection Services = new();
    static ILogger Logger = NullLogger.Instance;

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        var appName = "RunnerGetFiles";

        await CmdBootStrap.RunWithRunArgs(new SunamoCl.SunamoCmd.Args.RunArgs
        {
            IsLoggingToConsole = true,
            runInDebug = RunInDebug,
            categoryNameLogger = appName,
            ServiceCollection = Services,
            IsDebug =
#if DEBUG
            true
#else
false
#endif
        });

        //var sp = Services.BuildServiceProvider();
        //Logger = sp.GetService<ILogger>() ?? throw new Exception("Logger cannot be found");
    }

    static async Task RunInDebug()
    {
        var sp = Services.BuildServiceProvider();
        Logger = sp.GetService<ILogger>() ?? throw new Exception("Logger cannot be found");

        SHGetFilesTests t = new SHGetFilesTests();
        //t.GetFilesTest();

        //t.GetFoldersEveryFolderTest();
        t.GetFilesEveryFolderTest();

    }
}