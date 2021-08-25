using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float CalcDistance2D(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
}
