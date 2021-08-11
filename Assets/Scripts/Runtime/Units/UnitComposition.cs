using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTD.Units {
    public class UnitComposition : MonoBehaviour {
        [SerializeField]
        UnitComponentSettings[] overrideSettings = default;

        private void OnValidate() {
            var allComponents = GetComponentsInChildren<UnitComponent<UnitComponentSettings>>();
            foreach(var component in allComponents) {
                component.ApplySettings(GetSetting(component));
                Debug.Log($"Applied Setting to {component}");
            }
        }

        UnitComponentSettings GetSetting(UnitComponent<UnitComponentSettings> unitComponent) {
            return overrideSettings.Where(setting => setting.GetType() == unitComponent.settingType).FirstOrDefault();
        }
    }
}
