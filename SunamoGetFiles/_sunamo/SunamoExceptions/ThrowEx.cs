namespace SunamoGetFiles._sunamo.SunamoExceptions;

/// <summary>
/// Exception throwing helper methods
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws custom exception from Exception object
    /// </summary>
    /// <param name="ex">Exception to throw</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool Custom(Exception ex, bool isReallyThrowing = true)
    {
        return Custom(Exceptions.TextOfExceptions(ex), isReallyThrowing);
    }

    /// <summary>
    /// Throws custom exception with message
    /// </summary>
    /// <param name="message">Main exception message</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception</param>
    /// <param name="secondMessage">Additional message</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionMessage = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionMessage, isReallyThrowing);
    }

    #region Other
    /// <summary>
    /// Gets full name of currently executing code
    /// </summary>
    /// <returns>Full name with type and method</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Gets full name of executing code from type and method name
    /// </summary>
    /// <param name="type">Type object</param>
    /// <param name="methodName">Method name</param>
    /// <param name="isFromThrowEx">Whether called from ThrowEx</param>
    /// <returns>Full name with type and method</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type typeObject)
        {
            typeFullName = typeObject.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type typeInstance = type.GetType();
            typeFullName = typeInstance.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws exception if string is not null
    /// </summary>
    /// <param name="exception">Exception message</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }
    #endregion
}