using RTD.Hexagons;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTD.Hexgrid {
    public class HexMap : MonoBehaviour {

        [SerializeField]
        float size = 1;
        [SerializeField]
        Dimensions dimensions = Dimensions.XZ;
        [SerializeField]
        Orientation orientation = Orientation.PointyTop;
        [SerializeField]
        Transform context = default;

        [SerializeField, HideInInspector] HexDictionary map = new HexDictionary();

    public Vector3 GetWorldPosition(Hex3 hexagon) {
            var cartesian = Hex3.HexToCartesian(hexagon, orientation, size);
            switch (dimensions) {
                case Dimensions.XY:
                    return new Vector3(cartesian.x, cartesian.y, context.position.z);
                case Dimensions.XZ:
                    return new Vector3(cartesian.x, context.position.y, cartesian.y);
                case Dimensions.YZ:
                    return new Vector3(context.position.x, cartesian.x, cartesian.y);
                default:
                    throw new NotImplementedException($"{dimensions} have no behaviour defined");
            }
        }

        public Hex3 GetHexPosition(Vector3 worldPosition) {
            var position = dimensions switch
            {
                Dimensions.XY => new Vector2(worldPosition.x, worldPosition.y),
                Dimensions.XZ => new Vector2(worldPosition.x, worldPosition.z),
                Dimensions.YZ => new Vector2(worldPosition.y, worldPosition.z),
                _ => throw new NotImplementedException($"{dimensions} have no behaviour defined")
            };
            return Hex3.CartesianToHex(position, orientation, size);
        }

        public bool TryGetObject(Hex3 position, out GameObject gameObject) {
            return map.TryGetValue(position, out gameObject);
        }

        public bool Raycast(Ray ray, out Hex3 position) {
            var normal = dimensions switch
            {
                Dimensions.XY => Vector3.forward,
                Dimensions.XZ => Vector3.up,
                Dimensions.YZ => Vector3.right,
                _ => throw new NotImplementedException($"{dimensions} have no behaviour defined")
            };
            Plane plane = new Plane(context.rotation * normal, context.position);
            if (plane.Raycast(ray, out float enter)) {
                position = GetHexPosition(ray.GetPoint(enter));
                return true;
            } else {
                position = default;
                return false;
            }
        }

        public IEnumerable<(Hex3, GameObject)> GetAllObjects() {
            foreach(var hex in map) {
                yield return (hex.Key, hex.Value);
            }
        } 

        public Vector3 HexCorner(Hex3 hexagon, int index) {
            var center = GetWorldPosition(hexagon);
            float angleDegree = 60 * index - (orientation == Orientation.PointyTop ? 30 : 0);
            float angleRadians = (Mathf.PI / 180) * angleDegree;
            float x = size * Mathf.Cos(angleRadians);
            float y = size * Mathf.Sin(angleRadians);
            switch (dimensions) {
                case Dimensions.XY:
                    return center + new Vector3(x, y, 0);
                case Dimensions.XZ:
                    return center + new Vector3(x, 0, y);
                case Dimensions.YZ:
                    return center + new Vector3(0, x, y);
                default:
                    throw new NotImplementedException($"{dimensions} does not have a case");
            }
        }

        #region UnityEditor API
#if UNITY_EDITOR

        public void SetHexagonPrefab(Hex3 position, GameObject hexagonPrefab) {
            int group = Undo.GetCurrentGroup();
            DestroyObject(position);
            if (!hexagonPrefab) {
                return;
            }
            var newObject = PrefabUtility.InstantiatePrefab(hexagonPrefab) as GameObject;
            Undo.RegisterCreatedObjectUndo(newObject, "create Object");
            Undo.RegisterCompleteObjectUndo(this, "Add Hexagon");
            Undo.SetTransformParent(newObject.transform, context, "Set new Parent");
            newObject.transform.position = GetWorldPosition(position);
            map[position] = newObject;
            Undo.CollapseUndoOperations(group);
        }

        public void DestroyObject(Hex3 position) {
            int group = Undo.GetCurrentGroup();
            if (map.TryGetValue(position, out var oldObject)) {
                Undo.RegisterCompleteObjectUndo(this, "Destroy Hexagon");
                map.Remove(position);
                Undo.DestroyObjectImmediate(oldObject);
                Undo.CollapseUndoOperations(group);
            }
        }

        public void Clear() {
            Hex3[] copy = new Hex3[map.Keys.Count];
            map.Keys.CopyTo(copy, 0);
            foreach (var hex in copy) {
                DestroyObject(hex);
            }
        }

        public void DrawHexOutline(Hex3 hexagon, int corners = 6) {
            for (int i = 0; i < corners; i++) {
                var pointA = HexCorner(hexagon, i);
                var pointB = HexCorner(hexagon, (i + 1) % 6);
                Gizmos.DrawLine(context.position + pointA, context.position + pointB);
            }
        }
#endif
        #endregion
    }
}
