namespace POI.Application.Extensions;

public static class Check
{
    public static void EntityExists<TEntity>(TEntity? entity, string? errorMessage = null) where TEntity : class
    {
        if (entity == null)
            throw new InvalidOperationException(errorMessage ?? $"{typeof(TEntity).Name} could not be found.");
    }

    public static void IsNull(object? obj, string? errorMessage = null)
    {
        var objName = GetVariableName(obj);
        if (obj == null)
            throw new ArgumentNullException(objName, errorMessage ?? $"{objName} cannot be null.");
    }

    public static void IsNullOrEmpty(string value, string? errorMessage = null)
    {
        var valueName = GetVariableName(value);
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException(errorMessage ?? $"{valueName} cannot be null or empty.");
    }

    public static void IsNullOrWhiteSpace(string value, string? errorMessage = null)
    {
        var valueName = GetVariableName(value);
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(errorMessage ?? $"{valueName} cannot be null, empty or consist only of white-space characters.");
    }

    public static void IsGreaterThan(int value, int threshold, string? errorMessage = null)
    {
        var valueName = GetVariableName(value);
        if (value <= threshold)
            throw new ArgumentOutOfRangeException(valueName, errorMessage ?? $"{valueName} must be greater than {threshold}.");
    }

    private static string GetVariableName(object? obj)
    {
        return obj.GetType().Name;
    }
}