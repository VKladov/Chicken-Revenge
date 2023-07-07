using System;
using Scripts.Input;
using UnityEngine;

public class MouseInput : MonoBehaviour, IPlayerInput
{
    public event Action ShootPressed;
    public event Action ShootReleased;
    public event Action<Vector2> ScreenPositionChanged;
    public event Action ToggleAlingMode;

    private void Update()
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
        
        ScreenPositionChanged?.Invoke(Input.mousePosition);
    }
}