using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts
{
    public class BulletSpawner : IDisposable
    {
        private readonly BulletAligner _bulletAligner;
        private readonly ObjectsPool<Bullet> _pool;
        private readonly CancellationTokenSource _bulletFlyCancellation = new();

        public BulletSpawner(BulletAligner bulletAligner, ObjectsPool<Bullet> bulletsPool)
        {
            _bulletAligner = bulletAligner;
            _pool = bulletsPool;
        }
        
        public void Spawn(Bullet prefab, Vector3 position, Quaternion rotation)
        {
            var bullet = _pool.Get(prefab);
            bullet.transform.SetPositionAndRotation(position, rotation);
            ControlBullet(bullet, _bulletFlyCancellation.Token).Forget();
        }

        private async UniTaskVoid ControlBullet(Bullet bullet, CancellationToken cancellationToken)
        {
            _bulletAligner.AddBullet(bullet);
            await bullet.Fly(cancellationToken);
            _bulletAligner.RemoveBullet(bullet);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            
            _pool.Return(bullet);
        }

        public void Dispose()
        {
            _bulletFlyCancellation.Cancel();
            _bulletFlyCancellation.Dispose();
        }
    }
}