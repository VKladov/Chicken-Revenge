using System;
using Scripts.Enemies;
using UnityEngine;

namespace Scripts
{
    public class Obstacle : MonoBehaviour, IDamageReceiver
    {
        public event Action<Obstacle> Died;
        public void TakeDamage(int damage)
        {
            Died?.Invoke(this);
        }
    }
}