using System;
using Items;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class ItemSlotPair
    {
        public GameplayItemController itemController;
        public Vector2Int slot;
    }
}