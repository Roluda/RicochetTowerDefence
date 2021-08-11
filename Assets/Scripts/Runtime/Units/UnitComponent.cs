using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units {
    public abstract class UnitComponent : MonoBehaviour {
        public abstract Type settingType { get; }
        public abstract void ApplySettings(UnitComponentSettings settings);
        public abstract UnitComponentSettings currentSettings { get; }

        public Unit unit => observedUnit;

        [SerializeField]
        Unit observedUnit = default;

        protected virtual void OnValidate() {
            if (!observedUnit) {
                observedUnit = GetComponentInParent<Unit>();
            }
        }

        protected virtual void OnEnable() {
            observedUnit.RegisterUnitComponent(this);
        }

        protected virtual void OnDisable() {
            observedUnit.UnregisterUnitComponent(this);
        }
    }

    public class UnitComponent<TSetting> : UnitComponent where TSetting : UnitComponentSettings {
        public override Type settingType => typeof(TSetting);
        public override UnitComponentSettings currentSettings => settings;

        [SerializeField]
        public TSetting settings = default;

        public override void ApplySettings(UnitComponentSettings settings) {
            var set = settings as TSetting;
            Debug.Log(settings.GetType().Name + set + settingType.Name);
            this.settings = settings as TSetting;
            Debug.Log(this.settings);
        }
    }
}
