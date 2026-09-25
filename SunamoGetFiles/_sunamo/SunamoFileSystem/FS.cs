namespace SunamoGetFiles._sunamo.SunamoFileSystem;

internal class FS
{
    internal static string GetNormalizedExtension(string filename)
    {
        return NormalizeExtension(filename);
    }

    internal static string GetSizeInAutoString(double size)
    {
        var unit = ComputerSizeUnitsGetFiles.B;
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

        return $"{size} {unit}";
    }

    internal static DateTime LastModified(string path)
    {
        if (File.Exists(path))
        {
            return File.GetLastWriteTime(path);
        }
        return DateTime.MinValue;
    }

    internal static string MaskFromExtension(string extension = "*")
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string NormalizeExtension(string extension)
    {
        return "." + extension.TrimStart('.');
    }

    internal static void NormalizeExtensions(List<string> extensions)
    {
        for (int i = 0; i < extensions.Count; i++)
        {
            extensions[i] = NormalizeExtension(extensions[i]);
        }
    }

    internal static string GetFileName(string path)
    {
        return Path.GetFileName(path.TrimEnd('\\'));
    }
}
