using Scripts.Input;
using Zenject;

namespace Scripts.Weapon
{
    public class WeaponSwitch
    {
        private readonly PlayerShooter _shooter;
        private readonly Gun _rifle;
        private readonly Gun _autoGun;

        private IWeapon _current;
        private bool _triggerPressed;
        
        public WeaponSwitch(
            IPlayerInput input, 
            PlayerShooter shooter, 
            [Inject(Id = "Rifle")] Gun rifle, 
            [Inject(Id = "Auto")] Gun autoGun)
        {
            input.SwitchWeapon += SwitchWeapon;
            input.ShootPressed += InputOnShootPressed;
            input.ShootReleased += InputOnShootReleased;

            _shooter = shooter;
            _autoGun = autoGun;
            _rifle = rifle;

            _current = _autoGun;
            _autoGun.Hide();
            _rifle.Hide();
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
            _current.ReleaseTrigger();
            _current.Hide();

            IWeapon nextWeapon = _current == _autoGun ? _rifle : _autoGun;
            nextWeapon.Show();
            _shooter.TakeWeapon(nextWeapon);
            _current = nextWeapon;
            if (_triggerPressed)
            {
                nextWeapon.PressTrigger();
            }
        }
    }
}