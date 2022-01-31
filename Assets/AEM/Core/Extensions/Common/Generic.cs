using UnityEngine;

public static class GenericExtension
{
    /// <summary>
    /// Convenience function that swaps the values pointed to by x and y.
    /// </summary>
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}
