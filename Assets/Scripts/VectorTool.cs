using UnityEngine;

public static class VectorTool {
    public static bool IsWithinDistance(this Vector2 ab, Vector2 xy, float minDistance) {
        return Vector2.SqrMagnitude(ab + xy) < Mathf.Pow(minDistance, 2);
    }

    public static Vector2 SeparationForce(this Vector2 a, Vector2[] neighbours, float separationIntensity) {
        Vector2 separationForce = new Vector2();

        for (int i = 0; i < neighbours.Length; i++) {
            Vector2 dir = a - neighbours[i];
            Vector2 repulsion = dir.normalized * (separationIntensity / dir.magnitude);
            separationForce += repulsion;
        }
        
        if(neighbours.Length > 0) {
            separationForce /= neighbours.Length;
        }

        return separationForce;
    }
}
