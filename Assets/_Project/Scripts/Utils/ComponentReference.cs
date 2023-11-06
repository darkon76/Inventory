using Cysharp.Threading.Tasks;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.AddressableAssets;
#endif

namespace Utils
{
    public interface IComponentReference<T>
    {
        UniTask<T> LoadComponentAsync();
        UniTask<T> InstantiateComponentAsync(Vector3 position, Quaternion orientation, Transform parent = null);
        void ReleaseAsset();
        void ReleaseComponent(T component);
    }
    
    public class ComponentReference<T>: AssetReferenceT<T>,IComponentReference<T> where T: MonoBehaviour
    {
        public ComponentReference(string guid) : base(guid)
        {
        }
        
        public override bool ValidateAsset(Object obj)
        {
            var go = obj as GameObject;
            if (go == null)
            {
                return false;
            }

            var colliderHolder = go.GetComponent<T>();
            return colliderHolder != null;
        }

        public override bool ValidateAsset(string mainAssetPath)
        {
#if UNITY_EDITOR
            var asset = AssetDatabase.LoadAssetAtPath<T>(mainAssetPath);
            return asset != null;
#endif
            return base.ValidateAsset(mainAssetPath);
        }

        public async UniTask<T> LoadComponentAsync()
        {
            var result  = await LoadAssetAsync<GameObject>();
            return result.GetComponent<T>();
        }

        public async UniTask<T> InstantiateComponentAsync(Vector3 position, Quaternion orientation, Transform parent = null)
        {
            var result = await InstantiateAsync(position, orientation, parent);
            return result.GetComponent<T>();
        }
        public async UniTask<T> InstantiateComponentAsync(Transform parent = null, bool instantiateInWorldSpace = false)
        {
            var result = await InstantiateAsync(parent, instantiateInWorldSpace);
            return result.GetComponent<T>();
        }

        public void ReleaseComponent(T component)
        {
            ReleaseInstance(component.gameObject);
        }
    }
}