namespace SunamoGetFiles._sunamo.SunamoExceptions;

/// <summary>
/// Exception handling helper methods
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Checks and formats prefix text for exception messages
    /// </summary>
    /// <param name="before">Prefix text</param>
    /// <returns>Formatted prefix with colon and space, or empty string</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Gets text of exception including inner exceptions
    /// </summary>
    /// <param name="ex">Exception to process</param>
    /// <param name="isIncludingInner">Whether to include inner exceptions</param>
    /// <returns>Text of exception(s)</returns>
    internal static string TextOfExceptions(Exception ex, bool isIncludingInner = true)
    {
        if (ex == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(ex.Message);
        if (isIncludingInner)
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                stringBuilder.AppendLine(ex.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    /// <summary>
    /// Gets information about the place where exception occurred
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to fill first two output values</param>
    /// <returns>Tuple with type name, method name, and stack trace</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var lineIndex = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; lineIndex < lines.Count; lineIndex++)
        {
            var item = lines[lineIndex];
            if (isFillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts type and method name from stack trace line
    /// </summary>
    /// <param name="line">Stack trace line</param>
    /// <param name="type">Output: type name</param>
    /// <param name="methodName">Output: method name</param>
    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var trimmedText = line.Split("at ")[1].Trim();
        var fullMethodPath = trimmedText.Split("(")[0];
        var pathParts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = pathParts[^1];
        pathParts.RemoveAt(pathParts.Count - 1);
        type = string.Join(".", pathParts);
    }

    /// <summary>
    /// Gets name of calling method
    /// </summary>
    /// <param name="frameIndex">Stack frame index (1 = immediate caller)</param>
    /// <returns>Method name</returns>
    internal static string CallingMethod(int frameIndex = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameIndex)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    internal readonly static StringBuilder AdditionalInfoInnerStringBuilder = new();
    internal readonly static StringBuilder AdditionalInfoStringBuilder = new();
    #endregion

    #region OnlyReturnString
    /// <summary>
    /// Creates custom exception message with optional prefix
    /// </summary>
    /// <param name="before">Prefix text</param>
    /// <param name="message">Main message</param>
    /// <returns>Formatted message</returns>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
    #endregion
}
