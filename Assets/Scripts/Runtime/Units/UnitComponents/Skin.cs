using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Skin : UnitComponent<SkinSettings> {

        [SerializeField]
        Renderer currentRenderer = default;

        public void StartHighlight() {
            currentRenderer.gameObject.layer = settings.outlineLayer;
        }

        public void StopHighlight() {
            currentRenderer.gameObject.layer = settings.defaultLayer;
        }
    }
}
