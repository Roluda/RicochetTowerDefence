using RTD.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTD.CustomEditors {
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor {
        public override void OnInspectorGUI() {
            var manager = target as GameManager;
            base.OnInspectorGUI();
            if(GUILayout.Button(nameof(manager.LoadCurrentLevel))){
                manager.LoadCurrentLevel();
            }
            if (GUILayout.Button(nameof(manager.NextLevel))) {
                manager.NextLevel();
            }
            if (GUILayout.Button(nameof(manager.PlayerAction))) {
                manager.PlayerAction();
            }
        }
    }
}





