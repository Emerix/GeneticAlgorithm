using System;
using UnityEngine;

public class Genetics
{
    public static float[] DoCrossOver(float[] a, float[] b)
    {
        int length = a.Length;
        if (length != b.Length)
        {
            return null;
        }
        int crossOverIndex = UnityEngine.Random.Range(0, length);

        float[] output = new float[length];
        for (int i = 0; i < length; i++)
        {
            output[i] = i < crossOverIndex ? a[i] : b[i];
        }

        return output;
    }
    public static float[] DoMutation(float[] input, float mutationChance, Func<float>[] randomFunctions)
    {
        for (int i = 0; i < input.Length; i++)
        {
            bool mutate = UnityEngine.Random.value < mutationChance;

            if (mutate)
            {
                input[i] = randomFunctions[i]();
            }
        }

        return input;
    }
}