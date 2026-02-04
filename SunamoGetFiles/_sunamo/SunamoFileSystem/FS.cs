namespace SunamoGetFiles._sunamo.SunamoFileSystem;

/// <summary>
/// File system helper methods
/// </summary>
internal class FS
{
    /// <summary>
    /// Replaces invalid filename characters with empty string
    /// </summary>
    /// <param name="filename">Filename to clean</param>
    /// <returns>Cleaned filename</returns>
    internal static string ReplaceInvalidFileNameChars(string filename)
    {
        return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
    }

    /// <summary>
    /// Adds wildcard and extension dot if input contains only letters
    /// </summary>
    /// <param name="text">Input text</param>
    /// <returns>Formatted extension pattern</returns>
    internal static string AllIncludeIfOnlyLetters(string text)
    {
        text = text.ToLower().TrimStart('*').TrimStart('.');
        text = "*." + text;
        return text;
    }

    /// <summary>
    /// Gets normalized extension from filename
    /// </summary>
    /// <param name="filename">Filename</param>
    /// <returns>Normalized extension</returns>
    internal static string GetNormalizedExtension(string filename)
    {
        return NormalizeExtension(filename);
    }

    /// <summary>
    /// Gets size in automatically determined unit (B, KB, MB, GB, TB)
    /// </summary>
    /// <param name="size">Size in bytes</param>
    /// <returns>Formatted size string</returns>
    internal static string GetSizeInAutoString(double size)
    {
        ComputerSizeUnitsGetFiles unit = ComputerSizeUnitsGetFiles.B;
        if (size > NumConsts.KB)
        {
            unit = ComputerSizeUnitsGetFiles.KB;
            size /= NumConsts.KB;
        }
        if (size > NumConsts.KB)
        {
            unit = ComputerSizeUnitsGetFiles.MB;
            size /= NumConsts.KB;
        }
        if (size > NumConsts.KB)
        {
            unit = ComputerSizeUnitsGetFiles.GB;
            size /= NumConsts.KB;
        }
        if (size > NumConsts.KB)
        {
            unit = ComputerSizeUnitsGetFiles.TB;
            size /= NumConsts.KB;
        }

        return size + " " + unit.ToString();
    }

    /// <summary>
    /// Gets last modified time of file
    /// </summary>
    /// <param name="path">File path</param>
    /// <returns>Last write time or DateTime.MinValue if file doesn't exist</returns>
    internal static DateTime LastModified(string path)
    {
        if (File.Exists(path))
        {
            return File.GetLastWriteTime(path);
        }
        return DateTime.MinValue;
    }

    /// <summary>
    /// Creates file mask from extension
    /// </summary>
    /// <param name="extension">File extension</param>
    /// <returns>File mask pattern</returns>
    internal static string MascFromExtension(string extension = "*")
    {
        if (char.IsLetterOrDigit(extension[0]))
        {
            extension = "." + extension;
        }
        if (!extension.StartsWith("*"))
        {
            extension = "*" + extension;
        }
        if (!extension.StartsWith("*.") && extension.StartsWith("."))
        {
            extension = "*." + extension;
        }

        return extension;
    }

    /// <summary>
    /// Normalizes extension by ensuring it starts with dot
    /// </summary>
    /// <param name="text">Extension text</param>
    /// <returns>Normalized extension</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string NormalizeExtension(string text)
    {
        return "." + text.TrimStart('.');
    }

    /// <summary>
    /// Normalizes all extensions in list
    /// </summary>
    /// <param name="extensions">List of extensions</param>
    internal static void NormalizeExtensions(List<string> extensions)
    {
        for (int i = 0; i < extensions.Count; i++)
        {
            extensions[i] = NormalizeExtension(extensions[i]);
        }
    }

    /// <summary>
    /// Gets filename from path
    /// </summary>
    /// <param name="path">File path</param>
    /// <returns>Filename</returns>
    internal static string GetFileName(string path)
    {
        return Path.GetFileName(path.TrimEnd('\\'));
    }
}