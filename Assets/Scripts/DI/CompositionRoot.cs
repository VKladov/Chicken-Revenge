using System.Linq;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class CompositionRoot : MonoInstaller
    {
        [SerializeField] private Rect _playerArea;
        [SerializeField] private Player _player;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GunConfig[] _gunsConfigs;

        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().AsSingle();
            Container.Bind<PlayerShooter>().AsSingle();
            Container.Bind<WeaponSwitch>().AsSingle();
            Container.Bind<Camera>().FromInstance(_mainCamera);
            Container.Bind<Player>().FromInstance(_player);
            Container.BindInterfacesAndSelfTo<MouseInput>().AsSingle();
            Container.Bind<ScreenPositionConverter>().AsSingle();
            Container.Bind<Rect>().FromInstance(_playerArea);
            Container.Bind<PlayerAreaLimiter>().AsSingle();
            Container.Bind<BulletSpawner>().AsSingle();
            Container.Bind<BulletAligner>().AsSingle();
            Container.Bind<AlingMode>().AsSingle();
            Container.Bind<IWeapon[]>().FromMethod(CreateGuns).AsCached();
        }

        private Gun[] CreateGuns(InjectContext context)
        {
            var bulletSpawner = context.Container.Resolve<BulletSpawner>();
            return _gunsConfigs
                .Select(config => new Gun(bulletSpawner, config.View, config.BulletPrefab, config.ShotDelay))
                .ToArray();
        }
    }
}