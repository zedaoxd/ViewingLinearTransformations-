using UnityEngine;

public static class GizmosUtils 
{
    
    private const float defaultThickness = 8.0f;
    
    public static void DrawRay(Vector3 from, Vector3 direction, float width = defaultThickness)
    {
        DrawLine(from, from + direction, width);
    }

    public static void DrawLine(Vector3 p1, Vector3 p2, float width = defaultThickness)
    {
        var count = 1 + Mathf.CeilToInt(width); // how many lines are needed.
        if (count == 1)
        {
            Gizmos.DrawLine(p1, p2);
        }
        else
        {
            var c = Camera.current;
            if (c == null)
            {
                Debug.LogError("Camera.current is null");
                return;
            }
            var scp1 = c.WorldToScreenPoint(p1);
            var scp2 = c.WorldToScreenPoint(p2);

            var v1 = (scp2 - scp1).normalized; // line direction
            var n = Vector3.Cross(v1, Vector3.forward); // normal vector

            for (var i = 0; i < count; i++)
            {
                var o = 0.99f * n * width * ((float)i / (count - 1) - 0.5f);
                var origin = c.ScreenToWorldPoint(scp1 + o);
                var destiny = c.ScreenToWorldPoint(scp2 + o);
                Gizmos.DrawLine(origin, destiny);
            }
        }
    }

    public static void DrawVectorAtOrigin(Vector3 vec, float thickness = defaultThickness, bool drawArrowCap = true)
    {
        DrawVector(Vector3.zero, vec, thickness, drawArrowCap);
    }

    public static void DrawVector(Vector3 pos, Vector3 direction, float thickness = defaultThickness, bool drawArrowCap = true)
    {
        const float arrowHeadLength = 0.25f;
        const float arrowHeadAngle = 20.0f;

        DrawRay(pos, direction, thickness);

        if (drawArrowCap)
        {
            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            DrawRay(pos + direction, right * arrowHeadLength, thickness);
            DrawRay(pos + direction, left * arrowHeadLength, thickness);
        }
    }
}
