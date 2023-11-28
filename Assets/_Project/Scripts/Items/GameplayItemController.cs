using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Items
{
    public interface IGameplayItemController:IDisposable
    {
        void RotateCounter();
        void RotateWise();

        void SendToChest();

        Vector2Int ToGlobalTile(Vector2Int tilePosition);

        ICollection<Vector2Int> GetAllTiles();
    }
    
    public sealed class GameplayItemController:IGameplayItemController
    {
        private IGameplayItemView _view;
        private IItemData _itemData;
        private float _currentRotation;
        private Vector2Int _currentPosition;
        
        public GameplayItemController(
            IGameplayItemView view,
            IItemData itemData
        )
        {
            _view = view;
            _itemData = itemData;
        }

        public void Populate()
        {
            if (_view == null)
            {
                return;
            }
            var colliderAnchor = _view.CollidersAnchor;
            GameObject.Instantiate(_itemData.CollidersHolderPrefab, colliderAnchor);
            _view.Sprite = _itemData.Sprite;
            _view.SpriteOffset = _itemData.SpriteOffset;
            _view.IsKinematic = true;

            _view.BeginDragged += OnBeginDrag;
            _view.Dragged += OnDrag;
            _view.EndDragged += OnEndDrag;

            _view.Destroyed += OnViewDestroyed;

            _view.Controller = this;
        }

        private void OnViewDestroyed()
        {
            Dispose();
        }

        private void OnEndDrag(Vector2 point)
        {
        }

        private void OnDrag(Vector2 point)
        {
        }

        private void OnBeginDrag(Vector2 point)
        {
        }

        public void Dispose()
        {
            if (_view != null)
            {
                _view.BeginDragged -= OnBeginDrag;
                _view.Dragged -= OnDrag;
                _view.EndDragged -= OnEndDrag;

                _view.Destroyed -= OnViewDestroyed;
            
                _view.Destroy();
            }
        }

        public void RotateCounter()
        {
            _currentRotation += 90;
            if (_view != null)
            {
                _view.Rotation = _currentRotation;
            }
        }

        public void RotateWise()
        {
            _currentRotation -= 90;
            if (_view != null)
            {
                _view.Rotation = _currentRotation;
            }
        }

        public Vector2Int ToGlobalTile(Vector2Int tilePosition)
        {
            GetMatrix(out var matrix);
            var globalPosition = matrix.MultiplyVector(new Vector3(tilePosition.x, tilePosition.y));
            return globalPosition.RoundToVector2Int();
        }

        public ICollection<Vector2Int> GetAllTiles()
        {
            GetMatrix(out var matrix);
            List<Vector2Int> globalTilesPositions = new ();
            foreach (var tilePosition in _itemData.ItemTiles)
            {
                var globalPosition = matrix.MultiplyVector(new Vector3(tilePosition.x, tilePosition.y));
                globalTilesPositions.Add(globalPosition.RoundToVector2Int());
            }

            return globalTilesPositions;
        }

        private void GetMatrix(out Matrix4x4 matrix)
        {
            var position = new Vector3(_currentPosition.x, _currentPosition.y);
            var quaternion = Quaternion.Euler(new Vector3(0, 0, _currentRotation));
            var scale = Vector3.one;
            matrix = Matrix4x4.TRS(position, quaternion, scale);
        }
        
        public void SendToChest()
        {

        }
    }
}
