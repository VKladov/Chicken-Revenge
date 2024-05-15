using System;
using Zenject;

namespace Scripts
{
    public class PlayerShooter : IDisposable
    {
        private readonly IPlayerInput _input;
        private IWeapon _weapon;

        public PlayerShooter(IPlayerInput input)
        {
            _input = input;
            _input.ShootPressed += OnShootPressed;
            _input.ShootReleased += OnShootReleased;
        }

        private void OnShootPressed()
        {
            _weapon.PressTrigger();
        }

        private void OnShootReleased()
        {
            _weapon.ReleaseTrigger();
        }

        public void TakeWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void Dispose()
        {
            _input.ShootPressed -= OnShootPressed;
            _input.ShootReleased -= OnShootReleased;
        }
    }
}