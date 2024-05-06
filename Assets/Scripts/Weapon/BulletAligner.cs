using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class BulletAligner : ITickable
    {
        private readonly List<Bullet> _bulletsToControl = new(32);
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

        public void AddBullet(Bullet bullet)
        {
            _bulletsToControl.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            _bulletsToControl.Remove(bullet);
        }

        public void Tick()
        {
            if (!_enabled)
            {
                return;
            }
            
            var playerPosition = _player.transform.position;
            
            _bulletsToControl.ForEach(bullet =>
            {
                var bulletTransform = bullet.transform;
                var bulletPosition = bulletTransform.position;
                bulletTransform.position = new Vector3(playerPosition.x, bulletPosition.y, bulletPosition.z);
            });
        }
    }
}