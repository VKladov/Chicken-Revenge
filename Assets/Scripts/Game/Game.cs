using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Game : IDisposable, IInitializable
    {
        private readonly ObstaclesSpawner _obstaclesSpawner;
        private readonly PapuanSpawner _papuanSpawner;
        private readonly Grid _grid;
        
        public Game(ObstaclesSpawner obstaclesSpawner, PapuanSpawner papuanSpawner, Grid grid)
        {
            _obstaclesSpawner = obstaclesSpawner;
            _papuanSpawner = papuanSpawner;
            _grid = grid;
        }
        
        public void Initialize()
        {
            _papuanSpawner.PapuanDied += OnPapuanDied;
            
            SpawnCactuses();
            SpawnEnenies().Forget();
        }

        private void OnPapuanDied(Vector2Int cell)
        {
            _obstaclesSpawner.SpawnCactus(cell);
        }

        private void SpawnCactuses()
        {
            for (var row = 0; row < _grid.Rows; row++)
            {
                for (var col = 0; col < _grid.Columns; col++)
                {
                    if (Random.Range(0f, 1f) > 0.9f)
                    {
                        _obstaclesSpawner.SpawnCactus(new Vector2Int(col, row));
                    }
                }
            }
        }

        private async UniTaskVoid SpawnEnenies()
        {
            var cell = new Vector2Int(Random.Range(1, _grid.Columns - 1), _grid.Rows);
            for (var i = 0; i < 20; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
                _papuanSpawner.Spawn(cell);
            }
        }

        public void Dispose()
        {
            _papuanSpawner.PapuanDied -= OnPapuanDied;
        }
    }
}