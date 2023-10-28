using System;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventorySlotPreviewHandler
    {
        private readonly Dictionary<PreviewSlotState, PoolObjectContainer<SlotPreviewView>> previewSlotPool = new();

        public InventorySlotPreviewHandler(
            Transform parentTransform, IList<SlotPreviewView> slotPreviewViewsPrefabs)
        {
            var index = 0;
            var previewSlotStateValues = Enum.GetValues(typeof(PreviewSlotState));
            if (previewSlotStateValues.Length != slotPreviewViewsPrefabs.Count)
                Debug.LogError(
                    $"{nameof(InventorySlotPreviewHandler)} - Constructor: slotPreviewViesPrefabs lenght is different than the PreviewSlotState enum");
            foreach (PreviewSlotState previewSlotState in previewSlotStateValues)
            {
                var pool = new PoolObjectContainer<SlotPreviewView>(slotPreviewViewsPrefabs[index], parentTransform, 4);
                previewSlotPool[previewSlotState] = pool;
                index++;
            }
        }

        public bool TryGetSlotPreviewView(PreviewSlotState previewSlotState, out SlotPreviewView slotPreviewView)
        {
            if (!previewSlotPool.TryGetValue(previewSlotState, out var pool))
            {
                slotPreviewView = null;
                return false;
            }

            slotPreviewView = pool.Get();
            return true;
        }
    }
}