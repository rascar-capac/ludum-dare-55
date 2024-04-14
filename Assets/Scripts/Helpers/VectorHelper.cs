using UnityEngine;

public static class VectorHelper
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

    public static Vector2 GetRandomPositionInBelt(Vector2 center, float inner_radius, float outer_radius)
    {
        Vector2 unrotatedPosition = center + Random.Range(inner_radius, outer_radius) * Vector2.right;
        Vector2 positionInBelt = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * unrotatedPosition;

        return positionInBelt;
    }
}
