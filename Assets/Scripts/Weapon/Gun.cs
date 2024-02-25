using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts
{
    public class Gun : IWeapon
    {
        private readonly BulletSpawner _bulletSpawner;
        private readonly GunView _view;
        private readonly Bullet _bullet;
        private readonly float _shotDelay;
        
        private bool _triggerPressed;
        private bool _inCooldown;

        public Gun(BulletSpawner bulletSpawner, GunView view, Bullet bullet, float shotDelay)
        {
            _view = view;
            _bullet = bullet;
            _bulletSpawner = bulletSpawner;
            _shotDelay = shotDelay;
        }

        public void PressTrigger()
        {
            _triggerPressed = true;
            if (_inCooldown)
            {
                return;
            }
            
            StartFireLoop().Forget();
        }

        private async UniTask StartFireLoop()
        {
            while (_triggerPressed)
            {
                Shoot();
                _inCooldown = true;
                await UniTask.Delay(TimeSpan.FromSeconds(_shotDelay));
                _inCooldown = false;
            }
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

        private void Shoot()
        {
            var position = _view.GetNextShootPoint();
            var rotation = Quaternion.LookRotation(_view.transform.forward);
           _bulletSpawner.Spawn(_bullet, position, rotation);
        }
    }
}