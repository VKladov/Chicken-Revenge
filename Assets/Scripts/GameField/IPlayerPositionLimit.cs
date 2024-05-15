using UnityEngine;

namespace Scripts
{
    public interface IPlayerPositionLimit
    {
        public Vector3 ApplyPlayerLimits(Vector3 position);
    }
}