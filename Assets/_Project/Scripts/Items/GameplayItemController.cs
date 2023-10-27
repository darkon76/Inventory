using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Items
{
    public interface IGameplayItemController:IDisposable
    {
        void RotateCounter();
        void RotateWise();

        void SendToChest();
    }
    
    public sealed class GameplayItemController:IGameplayItemController
    {
        private IGameplayItemView _view;
        private IItemData _itemData;
        private float _currentRotation;
        
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
            var colliderAnchor = _view.CollidersAnchor;
            GameObject.Instantiate(_itemData.CollidersHolderPrefab, colliderAnchor);
            _view.Sprite = _itemData.Sprite;
            _view.IsKinematic = true;

            _view.BeginDragged += OnBeginDrag;
            _view.Dragged += OnDrag;
            _view.EndDragged += OnEndDrag;

            _view.Destroyed += OnViewDestroyed;
        }

        private void OnViewDestroyed()
        {
            Dispose();
        }

        private void OnEndDrag(PointerEventData obj)
        {
        }

        private void OnDrag(PointerEventData obj)
        {
        }

        private void OnBeginDrag(PointerEventData obj)
        {
        }

        public void Dispose()
        {
            _view.BeginDragged -= OnBeginDrag;
            _view.Dragged -= OnDrag;
            _view.EndDragged -= OnEndDrag;

            _view.Destroyed -= OnViewDestroyed;
            
            _view.Destroy();
        }

        public void RotateCounter()
        {
            _currentRotation += 90;
            _view.Rotation = _currentRotation;
        }

        public void RotateWise()
        {
            _currentRotation -= 90;
            _view.Rotation = _currentRotation;
        }

        public void SendToChest()
        {

        }
    }
}
