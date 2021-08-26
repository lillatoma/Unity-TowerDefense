using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float CalcDistance2D(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    public static float RealVector2Angle(Vector2 _in) //Like, why do you use weird logic, dear Vector2.Angle(...)?
    {
        float _out;
        _out = Mathf.Rad2Deg * Mathf.Atan2(_in.y, _in.x);
        return _out;
    }
}
