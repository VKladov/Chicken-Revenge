using System.Collections;
using UnityEngine;

namespace Scripts.Weapon.AutoGun
{
    public class AutoGun : IWeapon
    {
        
        private readonly BulletSpawner _bulletSpawner;
        private readonly BaseGunView _view;
        private readonly Bullet _bullet;
        private readonly float _shotDelay;

        private Coroutine _shootJob;
        private bool _canShoot = true;

        public AutoGun(BaseGunView view, Bullet bullet, BulletSpawner bulletSpawner, float shotDelay)
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
            
            if (_shootJob != null)
            {
                _view.StopCoroutine(_shootJob);
            }
            
            _shootJob = _view.StartCoroutine(ShootLoop(_shotDelay));

            _canShoot = false;
            _view.StartCoroutine(UnlockInSeconds(_shotDelay));
        }

        private void Shoot()
        {
            var bullet = _bulletSpawner.Spawn(_bullet);
            var rotation = Quaternion.LookRotation(_view.transform.forward);
            bullet.transform.SetPositionAndRotation(_view.transform.position + _view.ShotOffset, rotation);
        }

        private IEnumerator ShootLoop(float delay)
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator UnlockInSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);
            _canShoot = true;
        }

        public void ReleaseTrigger()
        {
            if (_shootJob != null)
            {
                _view.StopCoroutine(_shootJob);
            }
        }
    }
}