using UnityEngine;

namespace Scripts
{
    public class PlayerAreaLimiter
    {
        private readonly Rect _limits;
        
        public PlayerAreaLimiter(Rect limits)
        {
            _limits = limits;
        }

        public Vector3 ApplyLimits(Vector3 position)
        {
            var x = Mathf.Clamp(position.x, _limits.min.x, _limits.max.x);
            var z = Mathf.Clamp(position.z, _limits.min.y, _limits.max.y);
            return new Vector3(x, position.y, z);
        }
    }
}