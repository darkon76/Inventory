using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Inventory.ScriptableObjects
{
    public interface IPreviewSlotData
    {
        IList<SlotPreviewView> GetSlotPreviewViews();
    }
    
    [CreateAssetMenu(fileName = nameof(PreviewSlotData), menuName = "ScriptableObjects/PreviewSlotData")]
    public class PreviewSlotData: ScriptableObject, IPreviewSlotData
    {
        [SerializeField] private SlotPreviewView _freePrefab;
        [SerializeField] private SlotPreviewView _blockedPrefab; 
        [SerializeField] private SlotPreviewView _incompletePrefab; 
        [SerializeField] private SlotPreviewView _floatingPrefab;

        public IList<SlotPreviewView> GetSlotPreviewViews() => new List<SlotPreviewView>
        {
            _freePrefab,
            _blockedPrefab,
            _incompletePrefab,
            _floatingPrefab
        };
    }
}