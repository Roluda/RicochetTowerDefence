using UnityEngine;
using RTD.Units.UnitComponents;

namespace RTD.Units {
    public class UnitComponentFeature<T> : UnitComponentFeature where T : UnitComponent {
        [SerializeField]
        protected T observedComponent;

        protected virtual void OnValidate() {
            if (!observedComponent) {
                observedComponent = GetComponentInParent<T>();
            }
        }
        private void Awake() {
            if (!observedComponent) {
                observedComponent = GetComponentInParent<T>();
            }
        }

    }

    public abstract class UnitComponentFeature : MonoBehaviour {

    }
}
