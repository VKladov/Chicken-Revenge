using System;
using System.Linq;
using Zenject;

namespace Scripts
{
    public class WeaponSwitch : IDisposable
    {
        public class Factory : PlaceholderFactory<PlayerShooter, WeaponSwitch>
        {
            
        }
        
        private readonly PlayerShooter _shooter;
        private readonly IWeapon[] _weapons;
        private readonly IPlayerInput _input;

        private int _currentWeaponIndex;
        private bool _triggerPressed;

        private IWeapon CurrentWeapon => _weapons[_currentWeaponIndex];
        
        public WeaponSwitch(IPlayerInput input, PlayerShooter shooter, IWeapon[] weapons)
        {
            _input = input;
            _input.SwitchWeapon += SwitchWeapon;
            _input.ShootPressed += InputOnShootPressed;
            _input.ShootReleased += InputOnShootReleased;

            _shooter = shooter;
            _weapons = weapons;

            foreach (var gun in weapons)
            {
                gun.Hide();
            }
            SwitchWeapon();
        }

        private void InputOnShootPressed()
        {
            _triggerPressed = true;
        }

        private void InputOnShootReleased()
        {
            _triggerPressed = false;
        }

        private void SwitchWeapon()
        {
            CurrentWeapon.ReleaseTrigger();
            CurrentWeapon.Hide();

            _currentWeaponIndex = (_currentWeaponIndex + 1) % _weapons.Count();
            CurrentWeapon.Show();
            _shooter.TakeWeapon(CurrentWeapon);
            if (_triggerPressed)
            {
                CurrentWeapon.PressTrigger();
            }
        }

        public void Dispose()
        {
            _input.SwitchWeapon -= SwitchWeapon;
            _input.ShootPressed -= InputOnShootPressed;
            _input.ShootReleased -= InputOnShootReleased;
        }
    }
}