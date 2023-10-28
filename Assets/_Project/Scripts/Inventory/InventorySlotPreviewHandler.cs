using System;
using System.Collections.Generic;
using _Project.Scripts.Inventory.ScriptableObjects;
using ObjectPool;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventorySlotPreviewHandler
    {
        private readonly Dictionary<PreviewSlotState, PoolObjectContainer<SlotPreviewView>> previewSlotPool = new();

        public InventorySlotPreviewHandler(
            Transform parentTransform, IPreviewSlotData previewSlotData)
        {

            var previewSlotStateValues = Enum.GetValues(typeof(PreviewSlotState));
            var previewPrefabs = previewSlotData.GetSlotPreviewViews();

            var index = 0;
            foreach (PreviewSlotState previewSlotState in previewSlotStateValues)
            {
                if (previewSlotState == PreviewSlotState.Invalid)
                {
                    continue;
                }
                var pool = new PoolObjectContainer<SlotPreviewView>(previewPrefabs[index], parentTransform, 4);
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