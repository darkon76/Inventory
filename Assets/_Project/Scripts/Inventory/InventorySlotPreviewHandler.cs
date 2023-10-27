using ObjectPool;
using UnityEngine;


namespace _Project.Scripts.Inventory
{
    public class InventorySlotPreviewHandler
    {
        private readonly PoolObjectContainer<SlotPreviewView> freeSlotPool;
        private readonly PoolObjectContainer<SlotPreviewView> blockedSlotPool;
        private readonly PoolObjectContainer<SlotPreviewView> incompleteSlotPool;

        public InventorySlotPreviewHandler(
            Transform parentTransform,
            SlotPreviewView freeSlotPrefab, 
            SlotPreviewView blockedSlotPrefab,
            SlotPreviewView incompleteSlotPrefab)
        {
            freeSlotPool = new PoolObjectContainer<SlotPreviewView>(freeSlotPrefab, parentTransform, 4);
            blockedSlotPool = new PoolObjectContainer<SlotPreviewView>(blockedSlotPrefab, parentTransform, 4);
            incompleteSlotPool = new PoolObjectContainer<SlotPreviewView>(incompleteSlotPrefab, parentTransform, 4);
        }
        
       // public bool ValidPosition(IInventoryGridController gridController, )
    }
}