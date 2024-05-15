using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Scripts
{
    public class PapuanSpawner
    {
        public event Action<Vector2Int> PapuanDied;
        
        private readonly ObjectsPool<Papuan> _pool;
        private readonly Papuan _prefab;

        public PapuanSpawner(Papuan prefab, ObjectsPool<Papuan> papuansPool)
        {
            _prefab = prefab;
            _pool = papuansPool;
        }

        public void Spawn(Vector2Int cell)
        {
            var papuan = _pool.Get(_prefab);
            papuan.StartMove(cell);
            papuan.Died += OnPapuanDied;
        }

        private void OnPapuanDied(Papuan papuan)
        {
            papuan.Died -= OnPapuanDied;
            PapuanDied?.Invoke(papuan.GetCurrentCell());
            _pool.Return(papuan);
        }
    }
}