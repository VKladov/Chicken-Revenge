using Scripts.Input;
using Scripts.Weapon;
using UnityEngine;

namespace Scripts
{
    public class PlayerShooter
    {
        private readonly Player _player;
        private readonly BulletAligner _bulletAligner;
        private readonly IWeapon _defaultWeapon;

        private IWeapon _weapon;

        public PlayerShooter(IPlayerInput playerInput, Player player, IWeapon defaultWeapon)
        {
            _player = player;
            _defaultWeapon = defaultWeapon;
            _weapon = defaultWeapon;
            
            playerInput.ShootPressed += PlayerInputOnShootPressed;
            playerInput.ShootReleased += PlayerInputOnShootReleased;
        }

        private void PlayerInputOnShootPressed()
        {
            _weapon.PressTrigger();
        }

        private void PlayerInputOnShootReleased()
        {
            _weapon.ReleaseTrigger();
        }

        public void TakeWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }
    }
}