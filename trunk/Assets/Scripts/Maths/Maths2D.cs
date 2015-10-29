using UnityEngine;

public class Maths2D
{
	public static float CalcSignedAngle(Vector2 v1, Vector2 v2)
    {
        float perpDot = (v1.x * v2.y) - (v1.y * v2.x);

        return Mathf.Atan2(perpDot, Vector2.Dot(v1, v2));
    }
}