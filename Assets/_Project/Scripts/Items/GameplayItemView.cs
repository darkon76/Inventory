using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Items
{
    public interface IGameplayItemView: IDestroyed
    {
        event Action<Vector2> BeginDragged;
        event Action<Vector2> Dragged;
        event Action<Vector2> EndDragged; 
        bool IsKinematic { set;  }
        bool IsTrigger { set; }
        float Rotation { set; }
        Sprite Sprite { set; }
        Transform CollidersAnchor { get; }
        Vector2 SpriteOffset { set; }
        
        //TODO: Remove when the gizmo isn't needed.
        IGameplayItemController Controller { set; }

    }
    
    public sealed class GameplayItemView: MonoBehaviour, IGameplayItemView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _colliderAnchor;
        [Header("Config")]
        [SerializeField] private float _distanceToCamera;
        [Header("Debug")]
        //TODO: Remove this variables when the gizmo isn't used. 
        [SerializeField] private Vector2 _offset;
        private IItemData _itemData;
        //

        private IGameplayItemController _controller;

        public event Action<Vector2> BeginDragged;
        public event Action<Vector2> Dragged;
        public event Action<Vector2> EndDragged;
        
        public event Action Destroyed;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public bool IsKinematic
        {
            set => _rigidbody.isKinematic = value;
        }

        public bool IsTrigger
        {
            set
            {
                var colliders = gameObject.GetComponentsInChildren<Collider2D>();
                foreach (var currentCollider in colliders)
                {
                    currentCollider.isTrigger = value;
                }
            }
        }

        public float Rotation
        {
            set
            {
                var eulers = transform.eulerAngles;
                eulers.z = value;
                transform.eulerAngles = eulers;
            }
        }

        public Sprite Sprite
        {
            set => _spriteRenderer.sprite = value;
        }

        public Vector2 SpriteOffset
        {
            set
            {
                _spriteRenderer.gameObject.transform.localPosition = value;
                _colliderAnchor.localPosition = value;
            }
        }

        public Transform CollidersAnchor => _colliderAnchor;
        
        
        public IGameplayItemController Controller
        {
            set => _controller = value;
        }
        private void OnDrawGizmos()
        {
            if (_itemData != null)
            {
                DrawTile(Color.blue, _itemData.ItemTiles);
                DrawTile(Color.yellow, _itemData.ActivationTiles);
                DrawTile(Color.green, _itemData.SpecialTiles);
            }

            Gizmos.color = Color.red;
            var worldOffsetPosition = transform.TransformVector(_offset);
            Gizmos.DrawWireSphere(transform.position + worldOffsetPosition, 0.11f);

        }

        private void DrawTile(Color color, IReadOnlyCollection<Vector2Int> tiles)
        {
            if (tiles == null) return;
            
            Gizmos.color = color;
            foreach (var itemTile in tiles)
            {
                var tileOffset = _controller.ToGlobalTile(itemTile);
                Gizmos.DrawSphere(transform.position +  new Vector3(tileOffset.x, tileOffset.y), 0.1f);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var worldPoint = EventDataToPoint(eventData);
            BeginDragged?.Invoke(worldPoint);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var worldPoint = EventDataToPoint(eventData);
            Dragged?.Invoke(worldPoint);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var worldPoint = EventDataToPoint(eventData);
            EndDragged?.Invoke(worldPoint);
        }

        private Vector2 EventDataToPoint(in PointerEventData eventData)
        {
            var screenPosition = new Vector3(eventData.position.x, eventData.position.y, _distanceToCamera);
            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            var point = new Vector2(worldPosition.x, worldPosition.y);
            return point;
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }
      
        //TODO: Remove this when the gizmo isn't used. 
        [Conditional("UNITY_EDITOR")]
        public void SetItemData(IItemData itemData)
        {
            _offset = itemData.SpriteOffset;
            _itemData = itemData;
        }
        
        
    }
}