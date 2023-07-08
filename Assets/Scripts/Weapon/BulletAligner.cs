using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Weapon
{
    public class BulletAligner : ITickable
    {
        private readonly List<Bullet> _bulletsToControl = new();
        private readonly Player _player;
        private bool _enabled;

        public BulletAligner(Player player)
        {
            _player = player;
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void AddBulletToControl(Bullet bullet)
        {
            _bulletsToControl.Add(bullet);
            bullet.Destroyed += BulletOnDestroyed;
        }

        private void BulletOnDestroyed(Bullet bullet)
        {
            bullet.Destroyed -= BulletOnDestroyed;
            _bulletsToControl.Remove(bullet);
        }

        public void Tick()
        {
            if (!_enabled)
            {
                return;
            }
            _bulletsToControl.ForEach(bullet =>
            {
                var playerPosition = _player.transform.position;
                var bulletTransform = bullet.transform;
                var bulletPosition = bulletTransform.position;
                bulletTransform.position = new Vector3(playerPosition.x, bulletPosition.y, bulletPosition.z);
            });
        }
    }
}