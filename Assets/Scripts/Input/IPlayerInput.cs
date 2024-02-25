using System;
using UnityEngine;

namespace Scripts
{
    public interface IPlayerInput
    {
        public event Action ShootPressed;
        public event Action ShootReleased;
        public event Action<Vector2> ScreenPositionChanged;
        public event Action ToggleAlingMode;
        public event Action SwitchWeapon;
    }
}