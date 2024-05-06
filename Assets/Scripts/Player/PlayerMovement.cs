using System;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class PlayerMovement
    {
        private readonly Player _player;
        private readonly ScreenPositionConverter _positionConverter;
        private readonly PlayerAreaLimiter _areaLimiter; 
        private readonly IPlayerInput _playerInput; 
        
        public PlayerMovement(IPlayerInput playerInput, Player player, ScreenPositionConverter positionConverter, PlayerAreaLimiter playerAreaLimiter)
        {
            _player = player;
            _positionConverter = positionConverter;
            _areaLimiter = playerAreaLimiter;
            _playerInput = playerInput;
        }

        public void Update()
        {
            var groundPosition = _positionConverter.Convert(_playerInput.ScreenPosition);
            var limitedPosition = _areaLimiter.ApplyLimits(groundPosition);
            _player.transform.position = limitedPosition;
        }
    }
}