using UnityEngine;
using Zenject;

namespace Scripts
{
    public class CompositionRoot : MonoInstaller
    {
        [SerializeField] private Rect _playerArea;
        [SerializeField] private Player _player;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GunConfig _baseGunConfig;
        [SerializeField] private GunConfig _autoGunConfig;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera);
            Container.Bind<Player>().FromInstance(_player);
            Container.BindInterfacesAndSelfTo<MouseInput>().AsSingle();
            Container.Bind<ScreenPositionConverter>().AsSingle();
            Container.Bind<Rect>().FromInstance(_playerArea);
            Container.Bind<PlayerAreaLimiter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletAligner>().AsSingle();
            Container.Bind<BulletSpawner>().AsSingle();
            
            Container.Bind<PlayerMovement>().AsSingle().NonLazy();
            Container.Bind<PlayerShooter>().AsSingle().NonLazy();
            Container.Bind<AlingMode>().AsSingle().NonLazy();
            Container.Bind<WeaponSwitch>().AsSingle().NonLazy();
            Container.Bind<IWeapon[]>().FromMethod(CreateGuns).AsCached();
        }

        private IWeapon[] CreateGuns(InjectContext context)
        {
            var bulletSpawner = context.Container.Resolve<BulletSpawner>();
            return new IWeapon[]
            {
                new Gun(bulletSpawner, _baseGunConfig.View, _baseGunConfig.BulletPrefab, _baseGunConfig.ShotDelay),
                new Gun(bulletSpawner, _autoGunConfig.View, _autoGunConfig.BulletPrefab, _autoGunConfig.ShotDelay),
            };
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