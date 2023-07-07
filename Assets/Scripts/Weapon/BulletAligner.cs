using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Weapon
{
    public class BulletAligner
    {
        private readonly List<Bullet> _bulletsToControl = new();
        private readonly Player _player;
        private readonly IUpdateProvider _updateProvider;
        private bool _enabled;

        public BulletAligner(Player player, IUpdateProvider updateProvider)
        {
            _player = player;
            _updateProvider = updateProvider;
            Enable();
        }

        public void Enable()
        {
            if (_enabled)
            {
                return;
            }
            _updateProvider.OnUpdate += OnUpdate;
            _enabled = true;
        }

        public void Disable()
        {
            if (!_enabled)
            {
                return;
            }
            _updateProvider.OnUpdate -= OnUpdate;
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

        private void OnUpdate(float deltaTime)
        {
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