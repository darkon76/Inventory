using UnityEngine;

namespace _Project.Scripts.Items
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