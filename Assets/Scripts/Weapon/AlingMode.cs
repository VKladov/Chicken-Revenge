using System;

namespace Scripts
{
    public class AlingMode : IDisposable
    {
        private readonly BulletAligner _aligner;
        private readonly IPlayerInput _input;
        private bool _enabled;
        
        public AlingMode(BulletAligner aligner, IPlayerInput input)
        {
            _aligner = aligner;
            _input = input;
            _input.ToggleAlingMode += InputOnToggleAlingMode;
        }

        private void InputOnToggleAlingMode()
        {
            _enabled = !_enabled;
            if (_enabled)
            {
                _aligner.Enable();
            }
            else
            {
                _aligner.Disable();
            }
        }

        public void Dispose()
        {
            _input.ToggleAlingMode -= InputOnToggleAlingMode;
        }
    }
}