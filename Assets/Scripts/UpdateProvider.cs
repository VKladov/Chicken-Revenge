using System;
using UnityEngine;

namespace Scripts
{
    public class UpdateProvider : MonoBehaviour, IUpdateProvider
    {
        public event Action<float> OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke(Time.deltaTime);
        }
    }
}