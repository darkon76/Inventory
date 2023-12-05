using System;
using System.Collections.Generic;

namespace Utils
{
    public class WeightedRandom<T> where T: IWeight
    {
        private List<T> _items = new();
        private int _weightSum = 0;
        private Random _random;

        public WeightedRandom()
        {
            var seed = Guid.NewGuid().GetHashCode();
            _random = new Random(seed);
        }
        public WeightedRandom(int seed )
        {
            _random = new Random(seed);
        }

        public void SetSeed(int seed)
        {
            _random = new Random(seed);
        }
        
        public void Add(T item)
        {
            _items.Add(item);
            _weightSum += item.Weight;
        }

        public bool Remove(T item)
        {
            var contains = _items.Remove(item);
            if (contains)
            {
                _weightSum -= item.Weight;
            }

            return contains;
        }

        public T GetItem()
        {
            if (_weightSum == 0)
            {
                return default;
            }

            var randomValue = _random.Next(0, _weightSum);
            foreach (var item in _items)
            {
                var itemWeight = item.Weight;
                if (itemWeight > randomValue)
                {
                    return item;
                }

                randomValue -= itemWeight;
            }
            return default;
        }
    }
}