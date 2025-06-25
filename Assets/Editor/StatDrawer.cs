#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty baseValueProp = property.FindPropertyRelative("baseValue");
        SerializedProperty modifiersProp = property.FindPropertyRelative("modifiers");

        if (baseValueProp == null)
        {
            EditorGUI.LabelField(position, label.text, "Invalid Stat definition");
            return;
        }

        float baseValue = baseValueProp.floatValue;
        float modSum = 0;

        if (modifiersProp != null && modifiersProp.isArray)
        {
            for (int i = 0; i < modifiersProp.arraySize; i++)
            {
                modSum += modifiersProp.GetArrayElementAtIndex(i).floatValue;
            }
        }

        float total = baseValue + modSum;
        GUIContent tooltipLabel = new GUIContent($"{label.text} ({total})", label.tooltip);


        // Draw foldout
        property.isExpanded = EditorGUI.Foldout(
            new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            property.isExpanded,
            tooltipLabel,
            true
        );

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;
            Rect baseValueRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(baseValueRect, baseValueProp);

            Rect modifiersRect = new Rect(position.x, position.y + 2 * EditorGUIUtility.singleLineHeight, position.width, EditorGUI.GetPropertyHeight(modifiersProp));
            EditorGUI.PropertyField(modifiersRect, modifiersProp, true);
            EditorGUI.indentLevel--;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight; // for the foldout

        if (property.isExpanded)
        {
            SerializedProperty modifiersProp = property.FindPropertyRelative("modifiers");
            height += EditorGUIUtility.singleLineHeight; // baseValue
            height += EditorGUI.GetPropertyHeight(modifiersProp, true); // modifiers (supports list expansion)
        }

        return height;
    }
}
#endif