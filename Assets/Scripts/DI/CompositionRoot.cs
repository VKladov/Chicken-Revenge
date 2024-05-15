using System.Linq;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class CompositionRoot : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GunConfig[] _gunsConfigs;
        [SerializeField] private Grid _grid;
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private Papuan _papuanPrefab;

        public override void InstallBindings()
        {
            // Entry point
            Container.BindInterfacesAndSelfTo<Game>().AsSingle().NonLazy();
            
            Container.Bind<Camera>().FromInstance(_mainCamera);
            
            Container.Bind<Player>().FromInstance(_player);
            Container.Bind<PlayerMovement>().AsSingle();
            Container.Bind<ScreenPositionConverter>().AsSingle();
            
            Container.Bind<IWeapon[]>().FromMethod(CreateGuns).AsCached();
            Container.Bind<PlayerShooter>().AsSingle();
            Container.Bind<BulletSpawner>().AsSingle();
            Container.Bind<ObjectsPool<Bullet>>().AsSingle();
            Container.Bind<BulletAligner>().AsSingle();
            Container.Bind<WeaponSwitch>().AsSingle();
            Container.Bind<AlingMode>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Grid>().FromInstance(_grid);
            Container.BindInterfacesAndSelfTo<MouseInput>().AsSingle();
            
            Container.Bind<PapuanSpawner>().AsSingle();
            Container.Bind<ObjectsPool<Papuan>>().AsSingle();
            Container.Bind<Papuan>().FromInstance(_papuanPrefab);
            
            Container.Bind<ObjectsPool<Obstacle>>().AsSingle();
            Container.Bind<ObstaclesSpawner>().AsSingle();
            Container.Bind<Obstacle>().FromInstance(_obstaclePrefab);
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