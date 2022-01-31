using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Shuffle the ienumbrable and returns the shuffled list
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        T[] s = source.ToArray();
        for (int i = s.Length - 1; i >= 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            yield return s[swapIndex];
            s[swapIndex] = s[i];
        }
    }
}