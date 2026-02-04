// variables names: ok
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGetFiles.Tests;

/// <summary>
/// Dummy logger implementation for testing purposes
/// </summary>
internal class LoggerDummy : ILogger
{
    /// <summary>
    /// Singleton instance of LoggerDummy
    /// </summary>
    public static LoggerDummy Instance { get; set; } = new LoggerDummy();

    /// <summary>
    /// Begins a logical operation scope (not implemented in dummy logger)
    /// </summary>
    /// <typeparam name="TState">Type of state</typeparam>
    /// <param name="state">State object</param>
    /// <returns>Always returns null</returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    /// <summary>
    /// Checks if logging is enabled for specified log level
    /// </summary>
    /// <param name="logLevel">Log level to check</param>
    /// <returns>Always returns false (logging is disabled)</returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    /// <summary>
    /// Logs a message (no-op in dummy logger)
    /// </summary>
    /// <typeparam name="TState">Type of state</typeparam>
    /// <param name="logLevel">Log level</param>
    /// <param name="eventId">Event ID</param>
    /// <param name="state">State object</param>
    /// <param name="exception">Exception if any</param>
    /// <param name="formatter">Formatter function</param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {

    }
}
