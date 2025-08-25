namespace BaseSource.Kernel.Utilities.Extensions;

public static class ExtensionClaim
{
    public static bool IsAdminByName(this string? name,string value)
    {
        if (name is null)
            return false;
        if (name.Trim().ToLower() == value.ToLower())
            return true;
        return false;
    }

    public static int IsNullObject(this object? parameter)
    {
        if (parameter is null)
            return 0;
        return 1;
    }
}
