namespace SunamoGetFiles._sunamo.SunamoValues.Constants;

/// <summary>
/// Numeric constants
/// </summary>
internal class NumConsts
{
    /// <summary>
    /// Minus one constant
    /// </summary>
    internal const int MinusOne = -1;

    /// <summary>
    /// Default SMTP port if cannot be parsed
    /// </summary>
    internal const int DefaultPortIfCannotBeParsed = 587;

    /// <summary>
    /// Minimum age is 18 due to GDPR - below 18 is needed parent agreement of child
    /// </summary>
    internal const int MinAge = 18;

    /// <summary>
    /// DateTime minimum value representation
    /// </summary>
    internal static short DateTimeMinValue = 10101;

    /// <summary>
    /// DateTime maximum value representation
    /// </summary>
    internal static short DateTimeMaxValue = 32271;

    /// <summary>
    /// One thousand constant
    /// </summary>
    internal static int Thousand = 1000;

    /// <summary>
    /// Kilobyte constant (1024 bytes)
    /// </summary>
    internal const long KB = 1024;

    /// <summary>
    /// Zero as double
    /// </summary>
    internal const double ZeroDouble = 0;

    /// <summary>
    /// Zero as float
    /// </summary>
    internal const float ZeroFloat = 0;

    /// <summary>
    /// One constant (at int should be no postfix)
    /// </summary>
    internal const int One = 1;

    /// <summary>
    /// Zero as int
    /// </summary>
    internal const int ZeroInt = 0;
}