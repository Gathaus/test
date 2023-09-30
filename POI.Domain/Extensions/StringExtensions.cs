namespace POI.Domain.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Checks if the string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>True if the string is null, empty, or consists only of white-space characters, otherwise False.</returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
}