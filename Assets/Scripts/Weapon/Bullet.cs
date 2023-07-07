using System;
using UnityEngine;

namespace Scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> Destroyed;

        [SerializeField] private float _moveSpeed;

        private void Update()
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}