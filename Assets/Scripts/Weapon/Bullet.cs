using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private Transform _transform;
        private readonly RaycastHit[] _hits = new RaycastHit[10];

        public async UniTask Fly(CancellationToken cancellationToken)
        {
            _transform = transform;
            while (!cancellationToken.IsCancellationRequested)
            {
                var prevPosition = transform.position;
            
                var distance = _moveSpeed * Time.deltaTime;
                var direction = _transform.forward;
                _transform.position += direction * distance;

                var size = Physics.RaycastNonAlloc(prevPosition, direction, _hits, distance);
                if (size > 0)
                {
                    return;
                }

                await UniTask.Yield();
            }
        }
    }
}