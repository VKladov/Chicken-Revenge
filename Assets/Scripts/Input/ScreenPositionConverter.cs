using System;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Input
{
    public class ScreenPositionConverter
    {
        private Camera _camera;
        
        public ScreenPositionConverter(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 Convert(Vector2 screenPoint)
        {
            var ray = _camera.ScreenPointToRay(screenPoint);
            var pointOnGround = VectorUtils.RayPlaneIntersection(ray.origin, ray.direction, Vector3.zero, Vector3.up);
            return pointOnGround;
        }
    }
}