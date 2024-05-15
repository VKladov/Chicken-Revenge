using Scripts.Utils;
using UnityEngine;

namespace Scripts
{
    public class ScreenPositionConverter
    {
        private readonly Camera _camera;
        
        public ScreenPositionConverter(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 Convert(Vector2 screenPoint)
        {
            var ray = _camera.ScreenPointToRay(screenPoint);
            return ray.origin;
        }
    }
}