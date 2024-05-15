using System;
using UnityEngine;

namespace Scripts
{
    public interface IPlayerInput
    {
        Vector2 ScreenPosition { get; }
        event Action ShootPressed;
        event Action ShootReleased;
        event Action ToggleAlingMode;
        event Action SwitchWeapon;
        void Update();
    }
}