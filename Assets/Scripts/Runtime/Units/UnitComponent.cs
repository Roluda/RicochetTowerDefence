using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units {
    public class UnitComponent : MonoBehaviour {
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
}
