using Inventory;

namespace Managers
{
    public interface IGameCoordinator
    {
        void Update(float deltaTime);
        void Awake();
    }
    
    public class GameCoordinator: IGameCoordinator
    {
        private IInventoryGridController _gridController;
        
        public void Update(float deltaTime)
        {
            _gridController.DrawGizmos();
        }

        public void Awake()
        {
            _gridController = new InventoryGridController();
            _gridController.Initialize();
        }
    }
}