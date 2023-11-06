using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public sealed class InventoryGridHolder
    {
        [SerializeField] private Vector2Int _size;
    }
}