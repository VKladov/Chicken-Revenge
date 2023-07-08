using Scripts.Input;
using Scripts.Weapon;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Scripts.DI
{
    public class CompositeRoot : MonoInstaller
    {
        [SerializeField] private Rect _playerArea;
        [SerializeField] private Player _player;
        [SerializeField] private Camera _mainCamera;
        
        [FormerlySerializedAs("_baseGunView")]
        [Header("Base Gun Settings")]
        [SerializeField] private GunView _gunView;
        [SerializeField] private Bullet _baseGunBullet;
        [SerializeField] private float _baseGunShotDelay;
        
        [Header("Auto Gun Settings")]
        [SerializeField] private GunView _autoGunView;
        [SerializeField] private Bullet _autoGunBullet;
        [SerializeField] private float _autoGunShotDelay;

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

            Container.Bind<Gun>().WithId("Rifle").FromSubContainerResolve().ByMethod(InstallBaseGun).AsCached();
            Container.Bind<Gun>().WithId("Auto").FromSubContainerResolve().ByMethod(InstallAutoGun).AsCached();
        }

        private void InstallBaseGun(DiContainer subContainer)
        {
            subContainer.Bind<Gun>().AsSingle();
            subContainer.Bind<GunView>().FromInstance(_gunView);
            subContainer.Bind<Bullet>().FromInstance(_baseGunBullet);
            subContainer.BindInstance(_baseGunShotDelay);
        }

        private void InstallAutoGun(DiContainer subContainer)
        {
            subContainer.Bind<Gun>().AsSingle();
            subContainer.Bind<GunView>().FromInstance(_autoGunView);
            subContainer.Bind<Bullet>().FromInstance(_autoGunBullet);
            subContainer.BindInstance(_autoGunShotDelay);
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