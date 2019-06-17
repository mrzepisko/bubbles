using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomEditor(typeof(HexGrid))]
public class HexGridInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        HexGrid hexGrid = target as HexGrid;

        if (GUILayout.Button("Generate Hex Grid"))
            hexGrid.GenerateGrid();

        if (GUILayout.Button("Clear Hex Grid"))
            hexGrid.ClearGrid();
    }
}