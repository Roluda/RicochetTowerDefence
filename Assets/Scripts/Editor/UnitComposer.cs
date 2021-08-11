using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RTD.Units;
using System;
using System.Linq;

public class UnitComposer : EditorWindow
{
    [MenuItem("Tools/Unit Composer")]
    public static void ShowWindow() {
        DontDestroyOnLoad(GetWindow<UnitComposer>("Unit Composer"));
    }

    Unit currentUnit;

    UnitComponent[] components;
    UnitComponentSettings[] settings;

    Type[] types;
    int[] selections;
    bool editing;

    private void OnSelectionChange() {
        if(Selection.activeGameObject && Selection.activeGameObject.TryGetComponent<Unit>(out var unit)) {
            SelectUnit(unit);
        }
    }

    void SelectUnit(Unit unit) {
        if (PrefabUtility.IsPartOfPrefabAsset(unit)) {
            currentUnit = unit;
            components = currentUnit.GetComponentsInChildren<UnitComponent>();
            types = components.Select(comp => comp.settingType).ToArray();
            selections = new int[types.Length];
            int i = 0;
            foreach (var type in types) {
                selections[i] = settings
                    .Where(setting => setting.GetType() == type)
                    .ToList()
                    .IndexOf(components.Where(comp => comp.settingType == type).First().currentSettings);
                i++;
            }
            editing = false;
            Repaint();
        }
    }

    private void OnGUI() {
        if (!currentUnit) {
            return;
        }
        GUILayout.Label($"Selected Unit: {currentUnit.name}");
        if (!editing) {
            if(GUILayout.Button("Start Editing")){
                editing = true;
                AssetDatabase.OpenAsset(currentUnit.gameObject);
            }
        }
        if (editing) {
            int i = 0;
            foreach (var type in types) {
                var names = settings.Where(setting => setting.GetType() == type).Select(setting => setting.name).ToArray();
                GUILayout.Label(type.Name);
                selections[i] = GUILayout.SelectionGrid(selections[i], names, 3);
                i++;
            }
            i = 0;
            if (GUILayout.Button("Apply")) {
                foreach (var type in types) {
                    var select = settings.Where(setting => setting.GetType() == type).ToArray()[selections[i]];
                    foreach (var component in components.Where(comp => comp.settingType == select.GetType())) {
                        component.ApplySettings(select);
                        EditorUtility.SetDirty(component.gameObject);
                    }
                    i++;
                }
                AssetDatabase.SaveAssets();
            }
            if (GUILayout.Button("Close")) {
                editing = false;
            }
        }
    }

    private void OnEnable() {
        settings = Resources.LoadAll<UnitComponentSettings>("");
        if (Selection.activeGameObject && Selection.activeGameObject.TryGetComponent<Unit>(out var unit)) {
            SelectUnit(unit);
        }
    }


}
