using System;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public static class Vector2ExtensionMethods
    {
        public static Vector2Int RoundToInt(this Vector2 vector)
        {
            var x = (int)Math.Round(vector.x);
            var y = (int)Math.Round(vector.y);
            return new Vector2Int(x, y);
        }

        public static Vector2Int RoundToVector2Int(this Vector3 vector)
        {
            var x = (int)Math.Round(vector.x);
            var y = (int)Math.Round(vector.y);
            return new Vector2Int(x, y);
        }
    }
}