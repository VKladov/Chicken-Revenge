using UnityEngine;

namespace Scripts.Weapon
{
    public class BulletSpawner
    {
        private readonly BulletAligner _bulletAligner;
        
        public BulletSpawner(BulletAligner bulletAligner)
        {
            _bulletAligner = bulletAligner;
        }
        
        public Bullet Spawn(Bullet prefab)
        {
            var bullet = Object.Instantiate(prefab);
            _bulletAligner.AddBulletToControl(bullet);
            return bullet;
        }
    }
}