using System;
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

        public static Vector3 Add(this Vector3 vector3, Vector2 vector2)
        {
            return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y, vector3.z);
        }

        public static Vector3 Subtract(this Vector3 vector3, Vector2 vector2)
        {
            return new Vector3(vector3.x - vector2.x, vector3.y - vector2.y, vector3.z);
        }

        public static Vector3 MoveTowardsWithRemainder(this Vector3 current, Vector3 target, float maxDistanceDelta, out float remainder)
        {
            var num1 = target.x - current.x;
            var num2 = target.y - current.y;
            var num3 = target.z - current.z;
            var distanceSquared = num1 * num1 + num2 * num2 + num3 * num3;
            var distance = Mathf.Sqrt(distanceSquared);
            var maxDistanceDeltaSquared = maxDistanceDelta * maxDistanceDelta;
            if (distanceSquared == 0.0 || maxDistanceDelta >= 0.0 && distanceSquared <= maxDistanceDeltaSquared)
            {
                remainder = maxDistanceDelta - distance;
                return target;
            }
            
            remainder = 0;
            return new Vector3(current.x + num1 / distance * maxDistanceDelta, current.y + num2 / distance * maxDistanceDelta, current.z + num3 / distance * maxDistanceDelta);
        }
    }
}