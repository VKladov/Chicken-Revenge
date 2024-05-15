using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ObstaclesSpawner
    {
        private readonly Dictionary<Vector2Int, Obstacle> _obstacles = new();
        
        private readonly Obstacle _obstaclesPrefab;
        private readonly Grid _grid;
        private readonly ObjectsPool<Obstacle> _obstaclesPool;

        public ObstaclesSpawner(Obstacle obstaclesPrefab, Grid grid, ObjectsPool<Obstacle> pool)
        {
            _obstaclesPrefab = obstaclesPrefab;
            _grid = grid;
            _obstaclesPool = pool;
        }

        public void SpawnCactus(Vector2Int cell)
        {
            if (_obstacles.ContainsKey(cell))
            {
                Debug.LogWarning("Trying to spawn in busy cell! Aborted!");
                return;
            }
            
            var cactus = _obstaclesPool.Get(_obstaclesPrefab);
            cactus.transform.position = _grid.GetCellCenter(cell);
            _obstacles.Add(cell, cactus);
            cactus.Died += OnCactusDied;
        }

        private void OnCactusDied(Obstacle cactus)
        {
            cactus.Died -= OnCactusDied;
            
            // TODO: try to avoid loop
            foreach (var (key, value) in _obstacles)
            {
                if (value != cactus)
                {
                    continue;
                }
                _obstacles.Remove(key);
                _obstaclesPool.Return(cactus);
                return;
            }
        }

        public bool IsWalkable(Vector2Int cell)
        {
            return !_obstacles.ContainsKey(cell);
        }
    }
}