using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Weapon
{
    public class Gun : IWeapon
    {
        private readonly BulletSpawner _bulletSpawner;
        private readonly GunView _view;
        private readonly Bullet _bullet;
        private readonly float _shotDelay;
        
        private bool _triggerPressed;
        private bool _destroyed;

        public Gun(GunView view, Bullet bullet, BulletSpawner bulletSpawner, float shotDelay)
        {
            _view = view;
            _bullet = bullet;
            _bulletSpawner = bulletSpawner;
            _shotDelay = shotDelay;
            Loop().Forget();
        }

        public void PressTrigger()
        {
            _triggerPressed = true;
        }

        public void ReleaseTrigger()
        {
            _triggerPressed = false;
        }

        public void Show()
        {
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        private async UniTaskVoid Loop()
        {
            while (!_destroyed)
            {
                await UniTask.WaitUntil(() => _triggerPressed);
                while (_triggerPressed)
                { 
                    Shoot();
                    await UniTask.Delay(TimeSpan.FromSeconds(_shotDelay));
                }
            }
        }

        private void Shoot()
        {
            var bullet = _bulletSpawner.Spawn(_bullet);
            var rotation = Quaternion.LookRotation(_view.transform.forward);
            bullet.transform.SetPositionAndRotation(_view.GetNextShootPoint(), rotation);
        }
    }
}