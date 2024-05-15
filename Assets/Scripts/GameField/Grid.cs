using System;
using Scripts.Utils;
using UnityEngine;

namespace Scripts
{
    public class Grid : MonoBehaviour, IPlayerPositionLimit
    {
        [SerializeField] private Vector2Int _cells;
        [SerializeField] private float _cellWidth;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _playerMaxY;
        [SerializeField] private float _playerAreaOffset;

        public int Columns => _cells.x;
        public int Rows => _cells.y;

        public Vector3 GetCellCenter(Vector2Int cell)
        {
            return GetCellOrigin(cell) + new Vector3(_cellWidth * 0.5f, _cellWidth * 0.5f);
        }

        private Vector3 GetCellOrigin(Vector2Int cell)
        {
            var offset = new Vector3(cell.x * _cellWidth, cell.y * _cellWidth);
            return GetBottomLeftCorner() + offset;
        }

        private Vector3 GetBottomLeftCorner()
        {
            return _transform.position.Subtract(GetFullSize() * 0.5f);
        }

        private Rect GetFullRect()
        {
            var origin = GetCellOrigin(new Vector2Int(0, 0));
            var fullSize = GetFullSize();
            return new Rect(origin, fullSize);
        }

        private Vector2 GetFullSize()
        {
            return new Vector2((_cells.x + 1) * _cellWidth, (_cells.y + 1) * _cellWidth);
        }

        private Rect GetPlayerRect()
        {
            var rect = GetFullRect();
            rect.yMin += _playerAreaOffset;
            rect.height = _playerMaxY - _playerAreaOffset;
            rect.xMin += _playerAreaOffset;
            rect.width -= _playerAreaOffset;
            return rect;
        }

        public bool Contains(Vector2Int cell)
        {
            return cell.x >= 0 &&
                   cell.x <= _cells.x &&
                   cell.y >= 0 &&
                   cell.y <= _cells.y;
        }

        public Vector3 ApplyPlayerLimits(Vector3 position)
        {
            var rect = GetPlayerRect();
            var x = Mathf.Clamp(position.x, rect.min.x, rect.max.x);
            var y = Mathf.Clamp(position.y, rect.min.y, rect.max.y);
            return new Vector3(x, y, 0);
        }
        
        private void OnDrawGizmos()
        {
            var colorWas = Gizmos.color;
            Gizmos.color = Color.green;
            var rect = GetFullRect();
            DrawGizmosRect(rect);

            Gizmos.color = Color.blue;
            for (var x = 1; x <= _cells.x; x++)
            {
                var cellOrigin = GetCellOrigin(new Vector2Int(x, 0));
                Gizmos.DrawLine(new Vector3(cellOrigin.x, rect.min.y), new Vector3(cellOrigin.x, rect.max.y));
            }
            
            for (var y = 1; y <= _cells.y; y++)
            {
                var cellOrigin = GetCellOrigin(new Vector2Int(0, y));
                Gizmos.DrawLine(new Vector3(rect.min.x, cellOrigin.y), new Vector3(rect.max.x, cellOrigin.y));
            }
            
            Gizmos.color = Color.yellow;
            DrawGizmosRect(GetPlayerRect());
            Gizmos.color = colorWas;
        }

        private void DrawGizmosRect(Rect rect)
        {
            Gizmos.DrawLine(new Vector3(rect.min.x, rect.min.y), new Vector3(rect.min.x, rect.max.y));
            Gizmos.DrawLine(new Vector3(rect.min.x, rect.max.y), new Vector3(rect.max.x, rect.max.y));
            Gizmos.DrawLine(new Vector3(rect.max.x, rect.max.y), new Vector3(rect.max.x, rect.min.y));
            Gizmos.DrawLine(new Vector3(rect.max.x, rect.min.y), new Vector3(rect.min.x, rect.min.y));
        }
    }
}