using UnityEngine;

public static class MiscRandom
{
    public static float GetRandomSign()
    {
        return Random.value < 0.5f ? 1f : -1f;
    }
}
