using System.Diagnostics;
using UnityEngine;

namespace Utils.GizmoHelper
{
    public static class GizmoHelper
    {
        private static Color _color;
        private static GizmoHelperDrawer _gizmoHelperComp;


        public static Color Color
        {
            get => _color;
            set
            {
                _color = value;
                _gizmoHelperComp.Add(()=>{Gizmos.color = value;});
            }
        }
    
        [Conditional("UNITY_EDITOR")]
        public static void DrawCube(Vector3 center, Vector3 size)
        {
            _gizmoHelperComp.Add(()=>{Gizmos.DrawCube(center, size);});
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawLine(Vector3 @from, Vector3 to)
        {
            _gizmoHelperComp.Add(()=>{Gizmos.DrawLine(@from, to);});
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawRay(Vector3 @from, Vector3 direction)
        {
            _gizmoHelperComp.Add(()=>{Gizmos.DrawRay(@from, direction);});
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawSphere(Vector3 center, float radius)
        {
            _gizmoHelperComp.Add(()=>{Gizmos.DrawSphere(center, radius);});
        }
    
        [Conditional("UNITY_EDITOR")]
        public static void DrawSphere(Vector3 center, float radius, Color color)
        {
            Color = color;
            DrawSphere(center, radius);
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawWireCube(Vector3 center, Vector3 size)
        {
            _gizmoHelperComp.Add(()=>{Gizmos.DrawWireCube(center, size);});
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawWireSphere(Vector3 center, float radius)
        {
            _gizmoHelperComp.Add(()=>{Gizmos.DrawWireSphere(center, radius);});
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeMethodLoad()
        {
            var go = new GameObject("_GizmoHelper");
            GameObject.DontDestroyOnLoad(go);
            _gizmoHelperComp = go.AddComponent<GizmoHelperDrawer>();
        }

    }
}