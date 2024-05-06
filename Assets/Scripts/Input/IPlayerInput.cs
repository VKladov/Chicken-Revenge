using System;
using UnityEngine;

namespace Scripts
{
    public interface IPlayerInput
    {
        public Vector2 ScreenPosition { get; }
        public event Action ShootPressed;
        public event Action ShootReleased;
        public event Action ToggleAlingMode;
        public event Action SwitchWeapon;
        public void Update();
    }
}