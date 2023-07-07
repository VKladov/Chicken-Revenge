using System;

namespace Scripts
{
    public interface IUpdateProvider
    {
        public event Action<float> OnUpdate;
    }
}