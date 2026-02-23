// variables names: ok
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SunamoCl.SunamoCmd;
using SunamoGetFiles.Tests;

namespace RunnerGetFiles;

internal class Program
{
    static ServiceCollection Services = new();
    static ILogger Logger = NullLogger.Instance;

    static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(string[] args)
    {
        await CmdBootStrap.RunWithRunArgs(new SunamoCl.SunamoCmd.Args.RunArgs
        {
            RunInDebugAsync = RunInDebug,
            ServiceCollection = Services,
            IsDebug =
#if DEBUG
            true
#else
false
#endif
        });
    }

    static async Task RunInDebug()
    {
        var serviceProvider = Services.BuildServiceProvider();
        Logger = serviceProvider.GetService<ILogger>() ?? throw new Exception("Logger cannot be found");

        SHGetFilesTests tests = new SHGetFilesTests();
        tests.GetFilesEveryFolderTest();
    }
}
