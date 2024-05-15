using System;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class PlayerMovement
    {
        private readonly Player _player;
        private readonly ScreenPositionConverter _positionConverter;
        private readonly IPlayerPositionLimit _areaLimiter; 
        private readonly IPlayerInput _playerInput; 
        
        public PlayerMovement(IPlayerInput playerInput, Player player, ScreenPositionConverter positionConverter, IPlayerPositionLimit playerAreaLimiter)
        {
            _player = player;
            _positionConverter = positionConverter;
            _areaLimiter = playerAreaLimiter;
            _playerInput = playerInput;
        }

        public void Update()
        {
            var groundPosition = _positionConverter.Convert(_playerInput.ScreenPosition);
            var limitedPosition = _areaLimiter.ApplyPlayerLimits(groundPosition);
            _player.transform.position = limitedPosition;
        }
    }
}