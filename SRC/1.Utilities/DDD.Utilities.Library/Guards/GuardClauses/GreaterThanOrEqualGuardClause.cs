using DDD.Utilities.Library.Guards;

namespace DDD.Utilities.Library.Guards.GuardClauses;

public static class GreaterThanOrEqualGuardClause
{
    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T minimumValue, IComparer<T> comparer, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        int comparerResult = comparer.Compare(value, minimumValue);

        if (comparerResult < 0)
            throw new InvalidOperationException(message);
    }

    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T minimumValue, string message)
        where T : IComparable<T>, IComparable
    {
        guard.GreaterThanOrEqual(value, minimumValue, Comparer<T>.Default, message);
    }
}