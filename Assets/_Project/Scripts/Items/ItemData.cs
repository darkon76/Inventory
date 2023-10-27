using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Items
{
    public interface IItemData
    {
        string Name { get; }
        Vector2 SpriteOffset { get; }
        Sprite Sprite { get; }
        CollidersHolder CollidersHolderPrefab { get; }

        IReadOnlyCollection<Vector2Int> ItemTiles { get; }
        IReadOnlyCollection<Vector2Int> ActivationTiles { get; }
        IReadOnlyCollection<Vector2Int> SpecialTiles { get; }
    }
    
    [CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObjects/Item Data", order = 1)]
    public class ItemData: ScriptableObject, IItemData
    {
        [SerializeField] private string _name;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2Int[] _itemTiles;
        [SerializeField] private Vector2Int[] _activationTiles;
        [SerializeField] private Vector2Int[] _specialTiles;
        
        [SerializeField] private Sprite _sprite;
        [SerializeField] private CollidersHolder _colliderPrefab;

        public string Name => _name;
        public Vector2 SpriteOffset => _offset;
        public Sprite Sprite => _sprite;
        public CollidersHolder CollidersHolderPrefab => _colliderPrefab;
        public IReadOnlyCollection<Vector2Int> ItemTiles => _itemTiles;
        public IReadOnlyCollection<Vector2Int> ActivationTiles => _activationTiles;
        public IReadOnlyCollection<Vector2Int> SpecialTiles => _specialTiles;
    }
}