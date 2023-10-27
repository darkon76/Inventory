using UnityEngine;
namespace _Project.Scripts.Items
{
    public sealed class ItemFactory
    {
        public static GameplayItemController CreateItemFromData(GameplayItemView gameplayItemViewPrefab, ItemData itemDataSO)
        {
            var view = GameObject.Instantiate(gameplayItemViewPrefab);
            var gameplayView = view;
            gameplayView.SetItemData(itemDataSO);
            view.name = itemDataSO.Name;
            var gameplayItemController = new GameplayItemController(view, itemDataSO);
            gameplayItemController.Populate();
            return gameplayItemController;
        }
    }
}