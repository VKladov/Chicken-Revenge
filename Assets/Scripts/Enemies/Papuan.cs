using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Enemies;
using Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class Papuan : MonoBehaviour, IDamageReceiver
    {
        public event Action<Papuan>? Died;
        
        [SerializeField] private float _speed;
        
        private Vector2Int _currentCell;
        private ObstaclesSpawner _obstacles = null!;
        private Grid _grid = null!;
        private CancellationTokenSource? _moveCancellation = new();

        public Vector2Int GetCurrentCell() => _currentCell;

        [Inject]
        public void Construct(ObstaclesSpawner obstaclesSpawner, Grid grid)
        {
            _obstacles = obstaclesSpawner;
            _grid = grid;
        }

        private void OnDisable()
        {
            _moveCancellation?.Cancel();
            _moveCancellation?.Dispose();
            _moveCancellation = null;
        }

        public void StartMove(Vector2Int from)
        {
            _currentCell = from;
            transform.position = _grid.GetCellCenter(from);
            
            _moveCancellation?.Cancel();
            _moveCancellation?.Dispose();
            _moveCancellation = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
            Move(_moveCancellation.Token).Forget();
        }

        private async UniTaskVoid Move(CancellationToken cancellationToken)
        {
            var sideDirection = Vector2Int.right;
            var verticalDirection = Vector2Int.down;
            while (!cancellationToken.IsCancellationRequested)
            {
                var remainder = _speed * Time.deltaTime;
                while (CanMoveToCell(_currentCell + sideDirection))
                {
                    var nextHorizontalCell = _currentCell + sideDirection;
                    remainder = await MoveTo(nextHorizontalCell, remainder, cancellationToken);
                    _currentCell = nextHorizontalCell;
                }

                sideDirection *= -1;
                var nextVerticalCell = _currentCell + verticalDirection;
                if (!_grid.Contains(nextVerticalCell))
                {
                    verticalDirection *= -1;
                    nextVerticalCell = _currentCell + verticalDirection;
                }
                
                await MoveTo(nextVerticalCell, remainder, cancellationToken);
                _currentCell = nextVerticalCell;
            }
        }

        // Returns remainded distance
        private async UniTask<float> MoveTo(Vector2Int cell, float maxDelta, CancellationToken cancellationToken)
        {
            var targetPosition = _grid.GetCellCenter(cell);
            while (!cancellationToken.IsCancellationRequested)
            {
                transform.position = transform.position.MoveTowardsWithRemainder(targetPosition, maxDelta, out var remainder);
                if (remainder > 0f)
                {
                    return remainder;
                }
                await UniTask.Yield();
                maxDelta = _speed * Time.deltaTime;
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                transform.position = targetPosition;
            }

            return 0f;
        }

        private bool CanMoveToCell(Vector2Int cell)
        {
            return _obstacles.IsWalkable(cell) && _grid.Contains(cell);
        }

        public void TakeDamage(int damage)
        {
            _moveCancellation?.Cancel();
            _moveCancellation?.Dispose();
            _moveCancellation = null;
            Died?.Invoke(this);
        }
    }
}