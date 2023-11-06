using System.Reflection;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class DraggablePointDrawer : UnityEditor.Editor 
    {
        readonly GUIStyle style = new GUIStyle();
        void OnEnable()
        {
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
        }
        public void OnSceneGUI () 
        {
            var property = serializedObject.GetIterator ();
            while (property.Next (true)) 
            {
                if (property.propertyType == SerializedPropertyType.Vector3)
                {
                    var type = serializedObject.targetObject.GetType();
                    var fields = type.GetFields();
                    var field = type.GetField (property.name, (BindingFlags)(-1));
                    if (field == null) 
                    {
                        continue;
                    }
                    var draggablePoints = field.GetCustomAttributes (typeof(DraggablePoint), false);
                    if (draggablePoints.Length > 0) 
                    {
                        Handles.Label(property.vector3Value, property.name);
                        property.vector3Value = Handles.PositionHandle (property.vector3Value, Quaternion.identity);
                        serializedObject.ApplyModifiedProperties ();
                    }
                }
            }
        }
    }
}