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
            yield return new WaitForSeconds(2f);
            controller.RotateCounter();
            yield return new WaitForSeconds(2f);
            controller.RotateCounter();
            yield return new WaitForSeconds(2f);
            controller.RotateWise();
            yield return new WaitForSeconds(2f);
            controller.Dispose();
            yield return new WaitForSeconds(1);
        }
        
        private IGameplayItemController CreateItem()
        {
            _itemDataSO = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/_Project/Assets/Items/BackPack2x2.asset");
            _gameplayItemViewPrefab = AssetDatabase.LoadAssetAtPath<GameplayItemView>("Assets/_Project/Assets/Items/GameplayItem.prefab");

            return ItemFactory.CreateItemFromData(_gameplayItemViewPrefab, _itemDataSO);
        }
    }
}