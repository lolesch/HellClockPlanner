using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Code.Utility.AttributeRefs
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public sealed class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            _ = EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label, true);
    }
}
#endif
