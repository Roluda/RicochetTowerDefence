using RTD.Hexgrid;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace RTD.HexgridEditing {
    [CustomEditor(typeof(HexMap))]
    public class HexMapEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var map = (HexMap)target;
            if (GUILayout.Button("OpenMapEditor")) {
                HexBrushPalette.ShowWindow();
            }
        }

    }
}
