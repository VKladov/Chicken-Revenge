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
            _input.ShootPressed += InputOnShootPressed;
            _input.ShootReleased += InputOnShootReleased;
        }

        private void InputOnShootPressed()
        {
            _weapon.PressTrigger();
        }

        private void InputOnShootReleased()
        {
            _weapon.ReleaseTrigger();
        }

        public void TakeWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void Dispose()
        {
            _input.ShootPressed -= InputOnShootPressed;
            _input.ShootReleased -= InputOnShootReleased;
        }
    }
}