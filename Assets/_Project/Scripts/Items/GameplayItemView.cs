using System;
using System.Collections.Generic;
using System.Diagnostics;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Items
{
    public interface IGameplayItemView: IDestroyed
    {
        event Action<PointerEventData> BeginDragged;
        event Action<PointerEventData> Dragged;
        event Action<PointerEventData> EndDragged; 
        bool IsKinematic { set;  }
        bool IsTrigger { set; }
        float Rotation { set; }
        Sprite Sprite { set; }
        Transform CollidersAnchor { get; }
    }
    
    public sealed class GameplayItemView: MonoBehaviour, IGameplayItemView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody _rigidbody;
        [Header("Debug")]
        [SerializeField] private Vector2 _offset;

        public event Action<PointerEventData> BeginDragged;
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> EndDragged;
        
        public event Action Destroyed;
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

        public Transform CollidersAnchor => transform;

        private void OnDrawGizmosSelected()
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
                var tilePosition = _offset + itemTile;
                var tileOffset = transform.TransformVector( new Vector3(tilePosition.x, tilePosition.y));
                Gizmos.DrawSphere(transform.position + tileOffset, 0.1f);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragged?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Dragged?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragged?.Invoke(eventData);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }

        [SerializeField] private IItemData _itemData;
        
        [Conditional("UNITY_EDITOR")]
        public void SetItemData(IItemData itemData)
        {
            _offset = itemData.Offset;
            _itemData = itemData;

        }
        
        
    }
}