using System.Collections.Generic;

public static class ExtesionMethods
{
    #region VARIABLES

    private static System.Random rng = new System.Random();

    #endregion

    #region PUBLIC_METHODS

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    #endregion
}