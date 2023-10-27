using System.Collections;
using _Project.Scripts.Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

namespace _Project.Scripts.Editor.Tests
{
    public class GameplayItemControllerTests
    {
        private ItemData _itemDataSO;
        private GameplayItemView _gameplayItemViewPrefab;
        
        [UnityTest]
        public IEnumerator EditorUtility()
        {
            var controller = CreateItem();
            yield return new WaitForSeconds(0.5f);
            controller.RotateCounter();
            yield return new WaitForSeconds(10.5f);
            controller.RotateCounter();
            yield return new WaitForSeconds(0.5f);
            controller.RotateWise();
            yield return new WaitForSeconds(0.5f);
            controller.Dispose();
            yield return new WaitForSeconds(1);
        }
        
        private IGameplayItemController CreateItem()
        {
            _itemDataSO = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/_Project/Assets/Items/BackPack2x2.asset");
            _gameplayItemViewPrefab = AssetDatabase.LoadAssetAtPath<GameplayItemView>("Assets/_Project/Assets/Items/GameplayItem.prefab");
            
            var view = GameObject.Instantiate(_gameplayItemViewPrefab);
            var gameplayView = view;
            gameplayView.SetItemData(_itemDataSO);
            view.OnDrag(new PointerEventData(EventSystem.current));
            view.name = _itemDataSO.Name;
            var gameplayItemController = new GameplayItemController(view, _itemDataSO);
            gameplayItemController.Populate();
            return gameplayItemController;
        }
    }
}