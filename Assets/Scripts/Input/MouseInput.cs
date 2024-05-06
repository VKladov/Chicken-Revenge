using System;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class MouseInput : IPlayerInput
    {
        public Vector2 ScreenPosition { get; private set; }
        public event Action ShootPressed;
        public event Action ShootReleased;
        public event Action ToggleAlingMode;
        public event Action SwitchWeapon;
        
        public void Update()
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

            ScreenPosition = Input.mousePosition;
        }
    }
}