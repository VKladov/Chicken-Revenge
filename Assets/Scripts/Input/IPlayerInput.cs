using System;
using UnityEngine;

namespace Scripts.Input
{
    public interface IPlayerInput
    {
        public event Action ShootPressed;
        public event Action ShootReleased;
        public event Action<Vector2> ScreenPositionChanged;
        public event Action ToggleAlingMode;
    }
}