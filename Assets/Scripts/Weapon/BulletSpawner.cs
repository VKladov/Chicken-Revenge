using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts
{
    public class BulletSpawner
    {
        private readonly BulletAligner _bulletAligner;
        private readonly ObjectsPool<Bullet> _pool;

        public BulletSpawner(BulletAligner bulletAligner)
        {
            _bulletAligner = bulletAligner;
            _pool = new ObjectsPool<Bullet>();
        }
        
        public void Spawn(Bullet prefab, Vector3 position, Quaternion rotation)
        {
            var bullet = _pool.Get(prefab);
            bullet.transform.SetPositionAndRotation(position, rotation);
            BulletLife(bullet, bullet.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid BulletLife(Bullet bullet, CancellationToken cancellationToken)
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
    }
}