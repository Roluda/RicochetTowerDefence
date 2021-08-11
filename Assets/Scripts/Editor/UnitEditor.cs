using RTD.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTD.UnitEditing {
    [CustomEditor(typeof(Unit))]
    public class UnitEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if(GUILayout.Button("Open Settings Composer")) {
                UnitComposer.ShowWindow();
            }
        }
    }
}
