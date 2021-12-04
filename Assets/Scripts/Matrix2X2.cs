using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Matrix2X2
{
    [SerializeField] public float M00, M01, M10, M11;
    public static Matrix2X2 Identity => new Matrix2X2(1, 0, 0, 1);

    public Matrix2X2(float a, float b, float c, float d)
    {
        M00 = a;
        M01 = b;
        M10 = c;
        M11 = d;
    }

    public static Matrix2X2 operator *(in Matrix2X2 m, Matrix2X2 n)
    {
        return new Matrix2X2()
        {
            M00 = m.M00 * n.M00 + m.M01 * n.M10,
            M01 = m.M00 * n.M01 + m.M01 * n.M11,
            M10 = m.M10 * n.M00 + m.M11 * n.M10,
            M11 = m.M10 * n.M01 + m.M11 * n.M11,
        };
    }
    
    public static Vector2 operator *(in Matrix2X2 m, Vector2 n)
    {
        return new Vector2()
        {
            x = m.M00 * n.x + m.M01 * n.y,
            y = m.M10 * n.x + m.M11 * n.y,
        };
    }

    public override string ToString()
    {
        return $"|{M00} {M01}|\n|{M10} {M11}|";
    }
}
