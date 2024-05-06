using System;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class Player : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private PlayerShooter _playerShooter;
        private PlayerMovement _playerMovement;
        private WeaponSwitch _weaponSwitch;

        [Inject]
        public void Init(
            IPlayerInput playerInput, 
            PlayerShooter shooter, 
            PlayerMovement movement,
            WeaponSwitch weaponSwitch)
        {
            _playerInput = playerInput;
            _playerShooter = shooter;
            _playerMovement = movement;
            _weaponSwitch = weaponSwitch;
        }

        private void Update()
        {
            _playerInput.Update();
            _playerMovement.Update();
        }

        private void OnDestroy()
        {
            _playerShooter.Dispose();
            _weaponSwitch.Dispose();
        }
    }
}
