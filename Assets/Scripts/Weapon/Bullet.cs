using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Enemies;
using UnityEngine;

namespace Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage;

        private Transform _transform;
        private readonly RaycastHit2D[] _hits = new RaycastHit2D[10];

        public async UniTask Fly(CancellationToken cancellationToken)
        {
            _transform = transform;
            var direction = Vector3.up;
            while (!cancellationToken.IsCancellationRequested)
            {
                var prevPosition = transform.position;
            
                var distance = _moveSpeed * Time.deltaTime;
                _transform.position += direction * distance;

                var size = Physics2D.RaycastNonAlloc(prevPosition, direction, _hits, distance);
                if (size > 0)
                {
                    for (var i = 0; i < size; i++)
                    {
                        if (_hits[i].collider.TryGetComponent(out IDamageReceiver damageReceiver))
                        {
                            damageReceiver.TakeDamage(_damage);
                        }
                    }
                    return;
                }

                await UniTask.Yield();
            }
        }
    }
}