namespace Scripts
{
    public class AlingMode
    {
        private readonly BulletAligner _aligner;
        private bool _enabled;
        
        public AlingMode(BulletAligner aligner, IPlayerInput input)
        {
            _aligner = aligner;
            input.ToggleAlingMode += InputOnToggleAlingMode;
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
    }
}