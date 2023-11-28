using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class CollectionExtensionMethods
    {
        public static T Get2DValue<T>(this T[] collection, Vector2Int size, Vector2Int point)
        {
            var index = point.y * size.x + point.x;
            return collection[index];
        }
        public static T Get2DValue<T>(this List<T> collection, Vector2Int size, Vector2Int point)
        {
            var index = point.y * size.x + point.x;
            return collection[index];
        }

        public static void Set2DValue<T>(this T[] collection, Vector2Int size, Vector2Int point, T value)
        {
            var index = point.y * size.x + point.x;
            collection[index] = value;
        }
        
        public static void Set2DValue<T>(this List<T> collection, Vector2Int size, Vector2Int point, T value)
        {
            var index = point.y * size.x + point.x;
            collection[index] = value;
        }
    }
}