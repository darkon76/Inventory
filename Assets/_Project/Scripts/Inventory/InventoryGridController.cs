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
        private InventoryGrid _grid;

        public void DrawGizmos()
        {
            _grid.DrawGizmos();
        }

        public void Initialize()
        {
            _grid = new InventoryGrid(new Vector2Int(10, 5), new bool[10 * 5]);
        }
    }
}