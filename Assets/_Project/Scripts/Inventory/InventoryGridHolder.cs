﻿using System;
using _Project.Scripts.Items;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    [Serializable]
    public sealed class InventoryGridHolder
    {
        [SerializeField] private Vector2Int _size;
    }
}