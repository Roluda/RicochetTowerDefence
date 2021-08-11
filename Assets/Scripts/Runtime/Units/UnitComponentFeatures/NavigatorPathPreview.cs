using RTD.Units.UnitComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTD.Units.UnitComponentFeatures {
    public class NavigatorPathPreview : UnitComponentFeature<Navigator> {
        [SerializeField]
        LineRenderer lineRenderer = default;
        [SerializeField]
        float height = 0.5f;


        private void Update() {
            lineRenderer.enabled = observedComponent.hasPath;
            if (!observedComponent.hasPath) {
                return;
            }
            var path = Path();
            lineRenderer.positionCount = path.Length;
            lineRenderer.SetPositions(path);
        }

        Vector3[] Path() {
            var list = new List<Vector3>() { observedComponent.map.GetWorldPosition(observedComponent.position) };
            foreach(var hex in observedComponent.pathPreview) {
                list.Add(observedComponent.map.GetWorldPosition(hex) + Vector3.up * height);
            }
            return list.ToArray();
        }
    }
}
