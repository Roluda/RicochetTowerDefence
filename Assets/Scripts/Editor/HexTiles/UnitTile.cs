using RTD.Hexagons;
using RTD.Hexgrid;
using RTD.Units;
using UnityEditor;
using UnityEngine;

namespace RTD.HexgridEditing.Tiles {
    [CreateAssetMenu(fileName = "T_Unit_New", menuName = "HexTile/Unit")]
    public class UnitTile : HexTile {
        [SerializeField]
        Unit unit = default;
        [SerializeField]
        Color editorColor = Color.green;
        public override Texture GetPreviewTexture() {
            return AssetPreview.GetAssetPreview(unit.gameObject);
        }

        public override Color GetTelegraphColor() {
            return editorColor;
        }

        public override void PlaceTile(HexMap map, Hex3 position) {
            if(map.TryGetObject(position, out var go)) {
                var oldUnit = go.GetComponentInChildren<Unit>();
                if(oldUnit != null) {
                    Undo.DestroyObjectImmediate(oldUnit.gameObject);
                }
                var newUnit = PrefabUtility.InstantiatePrefab(unit, go.transform) as Unit;
                Undo.RegisterCreatedObjectUndo(newUnit.gameObject, "create Object");
            }
        }
    }
}