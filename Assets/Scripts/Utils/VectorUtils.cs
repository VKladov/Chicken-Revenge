using UnityEngine;

namespace Scripts.Utils
{
    public static class VectorUtils
    {
        public static Vector3 RayPlaneIntersection(Vector3 rayOrigin, Vector3 rayDirection, Vector3 plainPosition, Vector3 plainNormal)
        {
            rayDirection.Normalize();
            plainNormal.Normalize();
            var denominator = Vector3.Dot(rayDirection, -plainNormal);
            var distance = Vector3.Dot(plainPosition - rayOrigin, -plainNormal) / denominator;
            return rayOrigin + rayDirection * distance;
        }
    }
}