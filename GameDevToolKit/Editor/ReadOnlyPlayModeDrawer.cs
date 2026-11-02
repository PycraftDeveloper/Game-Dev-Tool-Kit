using UnityEngine;
using UnityEditor;

namespace GameDevToolKit.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyPlayModeAttribute))]
    public class ReadOnlyPlayModeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool previousGUIState = GUI.enabled;

            if (Application.isPlaying)
            {
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = previousGUIState;
        }
    }
}