using System;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class MouseInput : ITickable, IPlayerInput
    {
        public event Action ShootPressed;
        public event Action ShootReleased;
        public event Action<Vector2> ScreenPositionChanged;
        public event Action ToggleAlingMode;
        public event Action SwitchWeapon;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootPressed?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ShootReleased?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleAlingMode?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchWeapon?.Invoke();
            }
        
            ScreenPositionChanged?.Invoke(Input.mousePosition);
        }
    }
}