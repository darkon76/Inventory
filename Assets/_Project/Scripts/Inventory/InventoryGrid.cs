using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils.GizmoHelper;

namespace Inventory
{
    [Serializable]
    public sealed class InventoryGrid
    {
        private static readonly Vector3 GizmoOffset = Vector3.zero;
        private static readonly Vector2 GizmoOffsetMultiplier = Vector3.one;
        
        [SerializeField] 
        private Vector2Int _size;

        [SerializeField, HideInInspector]
        private bool[] _freeSlots;
        

        public InventoryGrid()
        {
            
        }
        public InventoryGrid(Vector2Int size, bool[] freeSlots)
        {
            _size = size;
            _freeSlots = freeSlots;
        }
        
        public PreviewSlotState CheckSlot(Vector2Int position)
        {
            if (position.x > _size.x || position.x < 0)
            {
                return PreviewSlotState.Invalid;
            }
            else if (position.y > _size.y || position.y < 0)
            {
                return PreviewSlotState.Invalid;
            }
            
            var isFree = _freeSlots.Get2DValue(_size, position);
            return isFree ? PreviewSlotState.Free : PreviewSlotState.Blocked;
        }

        public void SetSlotValue(Vector2Int position, bool value)
        {
            _freeSlots.Set2DValue(_size, position, value);
        }

        public void DrawGizmos()
        {
            for (int x = 0; x < _size.x; x++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    var freeSlot = _freeSlots.Get2DValue(_size, position );
                    GizmoHelper.Color = freeSlot ? Color.blue : Color.red;
                    Vector3 spherePosition = new()
                    {
                        x = position.x * GizmoOffsetMultiplier.x,
                        y = position.y * GizmoOffsetMultiplier.y
                    };
                    spherePosition += GizmoOffset;
                    GizmoHelper.DrawSphere( spherePosition, .1f );
                }
            }
        }
    }
}