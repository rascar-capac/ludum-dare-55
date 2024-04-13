using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return vector3;
    }

    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return vector2;
    }

    public static bool Approximately(Vector2 first_vector2, Vector2 second_vector2)
    {
        float distance = Vector2.Distance(first_vector2, second_vector2);

        return distance < 0.1f;
    }
}
