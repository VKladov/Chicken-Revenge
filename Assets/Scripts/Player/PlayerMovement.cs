using UnityEngine;

namespace Scripts
{
    public class PlayerMovement
    {
        private readonly Player _player;
        private readonly ScreenPositionConverter _positionConverter;
        private readonly PlayerAreaLimiter _areaLimiter; 
        
        public PlayerMovement(IPlayerInput playerInput, Player player, ScreenPositionConverter positionConverter, PlayerAreaLimiter playerAreaLimiter)
        {
            _player = player;
            _positionConverter = positionConverter;
            _areaLimiter = playerAreaLimiter;
            
            playerInput.ScreenPositionChanged += PlayerInputOnScreenPositionChanged;
        }

        private void PlayerInputOnScreenPositionChanged(Vector2 screenPosition)
        {
            var groundPosition = _positionConverter.Convert(screenPosition);
            var limitedPosition = _areaLimiter.ApplyLimits(groundPosition);
            _player.transform.position = limitedPosition;
        }
    }
}