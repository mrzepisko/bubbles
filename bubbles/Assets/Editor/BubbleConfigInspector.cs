using System.Collections;
using System.Collections.Generic;
using Bubbles.Config;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(BubbleConfig))]
public class BubbleConfigInspector : Editor {
    private const string PropertyItems = "items";
    private const string PropertyColor = "background";
    private const string HeaderLabel = "Color palette";
    
    private ReorderableList rl;

    private void OnEnable() {
        rl = new ReorderableList(serializedObject, serializedObject.FindProperty(PropertyItems));
        rl.drawElementCallback += DrawElementCallback;
        rl.drawHeaderCallback += rect => EditorGUI.LabelField(rect, HeaderLabel);
    }

    private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused) {
        var property = rl.serializedProperty.GetArrayElementAtIndex(index);
        var colorProperty = property.FindPropertyRelative(PropertyColor);
        Rect rLabel, rColor;
        rLabel = rColor = rect;
        rLabel.width = 30;
        rColor.x += rLabel.width;
        rColor.width = rect.width - rLabel.width;
        EditorGUI.LabelField(rLabel, $"2^{index}");
        var color = EditorGUI.ColorField(rColor, GUIContent.none, colorProperty.colorValue, true, false, false);
        color.a = 1f;
        colorProperty.colorValue = color;
        rl.serializedProperty.serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI() {
        rl.DoLayoutList();
    }
}
