using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class TwoWaySingleToManyDictionary<T,K>
    {
        private readonly Dictionary<T, HashSet<K>> _singleToMany = new();
        private readonly Dictionary<K, T> _manyToOne = new();

        public void Add(T single, K many)
        {
            _manyToOne[many] = single;
            if (!_singleToMany.TryGetValue(single, out var collection))
            {
                collection = new HashSet<K>();
                _singleToMany[single] = collection;
            }
            collection.Add(many);
        }

        public bool Remove(T single, K many)
        {
            if (!_manyToOne.Remove(many))
            {
                return false;
            }

            if (!_singleToMany.TryGetValue(single, out var collection))
            {
                Debug.LogWarning($"{nameof(TwoWaySingleToManyDictionary<T,K>)} - Remove: SingleToMany failed to remove the single, {single} - {many}");
                return false;
            }

            return collection.Remove(many);
        }
        

        public bool TryGet(K many, out T single)
        {
            return _manyToOne.TryGetValue(many, out single);
        }

        public bool TryGet(T single, out HashSet<K> many)
        {
            return _singleToMany.TryGetValue(single, out many);
        }

        public Dictionary<T, HashSet<K>>.Enumerator GetSinglesEnumerator()
        {
            return _singleToMany.GetEnumerator();
        }
        
        public Dictionary<K, T>.Enumerator GetManyEnumerator()
        {
            return _manyToOne.GetEnumerator();
        }
    }
}