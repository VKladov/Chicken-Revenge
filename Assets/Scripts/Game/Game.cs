using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Game
    {
        private readonly ObstaclesSpawner _obstaclesSpawner;
        private readonly PapuanSpawner _papuanSpawner;
        private readonly Grid _grid;
        
        public Game(ObstaclesSpawner obstaclesSpawner, PapuanSpawner papuanSpawner, Grid grid)
        {
            _obstaclesSpawner = obstaclesSpawner;
            _papuanSpawner = papuanSpawner;
            _grid = grid;
            Start().Forget();
            
            _papuanSpawner.PapuanDied += PapuanSpawnerOnPapuanDied;
        }

        private void PapuanSpawnerOnPapuanDied(Vector2Int cell)
        {
            _obstaclesSpawner.SpawnCactus(cell);
        }

        private async UniTaskVoid Start()
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

            var cell = new Vector2Int(Random.Range(1, _grid.Columns - 1), _grid.Rows);
            for (var i = 0; i < 20; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
                _papuanSpawner.Spawn(cell);
            }
        }
    }
}