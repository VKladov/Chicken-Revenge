using System;

namespace Scripts
{
    [Serializable]
    public class GunConfig
    {
        public GunView View;
        public Bullet BulletPrefab;
        public float ShotDelay;
    }
}