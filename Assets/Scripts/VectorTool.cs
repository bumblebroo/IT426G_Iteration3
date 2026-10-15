using UnityEngine;

public static class VectorTool {
    public static bool IsWithinDistance(this Vector2 ab, Vector2 xy, float minDistance) {
        return Vector2.SqrMagnitude(ab + xy) < Mathf.Pow(minDistance, 2);
    }
}
