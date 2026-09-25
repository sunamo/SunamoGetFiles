namespace SunamoGetFiles._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool Custom(Exception ex, bool isReallyThrowing = true)
    {
        return Custom(Exceptions.TextOfExceptions(ex), isReallyThrowing);
    }

    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        var joinedMessage = string.Join(" ", message, secondMessage);
        string? exceptionMessage = Exceptions.Custom(FullNameOfExecutedCode(), joinedMessage);
        return ThrowIsNotNull(exceptionMessage, isReallyThrowing);
    }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        var placeOfException = Exceptions.PlaceOfException();
        var fullName = fullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string fullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
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
            var typeInstance = type.GetType();
            typeFullName = typeInstance.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exceptionMessage, bool isReallyThrowing = true)
    {
        if (exceptionMessage != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exceptionMessage);
            }
            return true;
        }
        return false;
    }
    #endregion
}
