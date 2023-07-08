using Scripts.Input;
using Scripts.Weapon;

namespace Scripts
{
    public class PlayerShooter
    {
        private readonly Player _player;
        private readonly BulletAligner _bulletAligner;

        private IWeapon _weapon;

        public PlayerShooter(IPlayerInput playerInput, Player player)
        {
            _player = player;
            
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