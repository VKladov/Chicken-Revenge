using UnityEngine;

namespace Scripts.Weapon
{
    public class GunView : MonoBehaviour
    {
        [SerializeField] private Vector3[] _shootPoints;
        
        private int _shotPointIndex;

        public Vector3 GetNextShootPoint()
        {
            _shotPointIndex++;
            _shotPointIndex %= _shootPoints.Length;
            return transform.position + _shootPoints[_shotPointIndex];
        }
    }
}