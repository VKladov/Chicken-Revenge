using System;
using UnityEngine;

namespace Scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> Destroyed;

        [SerializeField] private float _moveSpeed;

        private Vector3 _lastPosition;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _lastPosition = _transform.position;
        }

        private void Update()
        {
            _lastPosition = transform.position;
            var position = _transform.position;
            position += _transform.forward * _moveSpeed * Time.deltaTime;
            _transform.position = position;
            Raycast(_lastPosition, position);
        }

        private void Raycast(Vector3 from, Vector3 to)
        {
            var hits = Physics.RaycastAll(from, (to - from).normalized, Vector3.Distance(from, to));
            foreach (var raycastHit in hits)
            { 
                if (raycastHit.transform.TryGetComponent(out GameFieldsWall wall))
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}