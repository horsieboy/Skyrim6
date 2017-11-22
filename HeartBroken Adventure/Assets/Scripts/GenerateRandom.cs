using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandom
{

    public int minValue;

    public int maxValue;

    public GenerateRandom(int min, int max)
    {
        min = minValue;
        max = maxValue;
    }

    public int GetrandomValue
    {

        get { return Random.Range(minValue, maxValue); }

    }
}
