using UnityEngine;

namespace Items
{
    public class ItemFactoryTest:MonoBehaviour
    {
        [SerializeField] private ItemData _itemDataSO;
        [SerializeField] private GameplayItemView _gameplayItemViewPrefab;
        
        [ContextMenu(nameof(CreateItem))]
        public void CreateItem()
        {
            ItemFactory.CreateItemFromData(_gameplayItemViewPrefab, _itemDataSO);
        }
    }
}