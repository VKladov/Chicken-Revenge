using System.Collections;
using UnityEngine;

namespace Scripts.Weapon
{
    public class BaseGun : IWeapon
    {
        private bool _canShoot = true;
        
        private readonly BulletSpawner _bulletSpawner;
        private readonly BaseGunView _view;
        private readonly Bullet _bullet;
        private readonly float _shotDelay;

        public BaseGun(BaseGunView view, Bullet bullet, BulletSpawner bulletSpawner, float shotDelay)
        {
            _view = view;
            _bullet = bullet;
            _bulletSpawner = bulletSpawner;
            _shotDelay = shotDelay;
        }
            
        public void PressTrigger()
        {
            if (!_canShoot)
            {
                return;
            }

            var bullet = _bulletSpawner.Spawn(_bullet);
            var rotation = Quaternion.LookRotation(_view.transform.forward);
            bullet.transform.SetPositionAndRotation(_view.transform.position + _view.ShotOffset, rotation);
            _canShoot = false;
            _view.StartCoroutine(UnlockInSeconds(_shotDelay));
        }

        private IEnumerator UnlockInSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);
            _canShoot = true;
        }

        public void ReleaseTrigger()
        {
            
        }
    }
}