using UnityEngine;

public static class GetRandomStuff<T>
{
    public static T GetRandomThing(GetRandomStuffType<T>[] stuff)
    {
        if (stuff.Length == 0) return default;

        int totalIncidence = 0;
        foreach (GetRandomStuffType<T> blob in stuff)
        {
            totalIncidence += blob.Rate;
        }
        int randomIncidence = Random.Range(0, totalIncidence);

        int currentIncidence = stuff[0].Rate;
        foreach (GetRandomStuffType<T> blob in stuff)
        {
            if (randomIncidence < currentIncidence)
            {
                return blob.Stuff;
            }
            currentIncidence += blob.Rate;
        }

        return default;
    }
}

public class GetRandomStuffType<T>
{
    public readonly T Stuff;
    public readonly int Rate;

    public GetRandomStuffType(T stuff, int rate)
    {
        Stuff = stuff;
        Rate = rate;
    }
}