using System;
using Items;
using UnityEngine;

namespace Inventory
{
    public interface IInventoryGridController
    {
        void DrawGizmos();
        void Initialize();
    }
    
    public class InventoryGridController: IInventoryGridController
    {
        private static readonly Vector2Int INVENTORY_SIZE = new Vector2Int(10, 5);
        private InventoryGrid _mainGrid;
        private InventoryGrid _bagsGrid;
        
        public void DrawGizmos()
        {
         //   _mainGrid.DrawGizmos();
        }

        public void Initialize()
        {
            var size = INVENTORY_SIZE.x * INVENTORY_SIZE.y;
            _mainGrid = new InventoryGrid(INVENTORY_SIZE, new bool[size]);
            _bagsGrid = new InventoryGrid(INVENTORY_SIZE, new bool[size]);
        }

        public PreviewSlotState GetItemSlotPreview(Vector2Int slot)
        {
            var mainGridPreview = _mainGrid.CheckSlot(slot);
            switch (mainGridPreview)
            {
                case PreviewSlotState.Invalid:
                    return PreviewSlotState.Invalid;
                case PreviewSlotState.Free:
                    return PreviewSlotState.Floating;
                case PreviewSlotState.Blocked:
                    break;
                case PreviewSlotState.Incomplete:
                case PreviewSlotState.Floating:
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            //The main grid is blocked because it has a bag checking the bag. 
            var bagGridPreview = _bagsGrid.CheckSlot(slot);
            return bagGridPreview;
        }

        public void SetItem(GameplayItemController item, Vector2Int slot)
        {
            //TODO:
        }
        
    }
}