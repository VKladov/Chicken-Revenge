using Scripts.Input;
using Scripts.Weapon;
using Scripts.Weapon.AutoGun;
using UnityEngine;

namespace Scripts.DI
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private Rect _playerArea;
        [SerializeField] private Player _player;
        [SerializeField] private MouseInput _mouseInput;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private UpdateProvider _updateProvider;
        
        [Header("Base Gun Settings")]
        [SerializeField] private BaseGunView _baseGunView;
        [SerializeField] private Bullet _baseGunBullet;
        [SerializeField] private float _baseGunShotDelay;
        
        [Header("Auto Gun Settings")]
        [SerializeField] private BaseGunView _autoGunView;
        [SerializeField] private Bullet _autoGunBullet;
        [SerializeField] private float _autoGunShotDelay;
        
        private void Start()
        {
            var positionConverter = new ScreenPositionConverter(_mainCamera);
            var limits = new PlayerAreaLimiter(_playerArea);
            var movement = new PlayerMovement(_mouseInput, _player, positionConverter, limits);
            var bulletAligner = new BulletAligner(_player, _updateProvider);
            var bulletSpawner = new BulletSpawner(bulletAligner);
            
            var baseGun = new BaseGun(_baseGunView, _baseGunBullet, bulletSpawner, _baseGunShotDelay);
            var autoGun = new AutoGun(_autoGunView, _autoGunBullet, bulletSpawner, _autoGunShotDelay);
            var shooter = new PlayerShooter(_mouseInput, _player, autoGun);
            var alingMode = new AlingMode(bulletAligner, _mouseInput);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(new Vector3(_playerArea.min.x, 0, _playerArea.min.y), new Vector3(_playerArea.max.x, 0, _playerArea.min.y));
            Gizmos.DrawLine(new Vector3(_playerArea.max.x, 0, _playerArea.min.y), new Vector3(_playerArea.max.x, 0, _playerArea.max.y));
            Gizmos.DrawLine(new Vector3(_playerArea.max.x, 0, _playerArea.max.y), new Vector3(_playerArea.min.x, 0, _playerArea.max.y));
            Gizmos.DrawLine(new Vector3(_playerArea.min.x, 0, _playerArea.max.y), new Vector3(_playerArea.min.x, 0, _playerArea.min.y));
        }
    }
}