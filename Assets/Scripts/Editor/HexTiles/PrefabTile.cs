using RTD.Hexagons;
using RTD.Hexgrid;
using UnityEditor;
using UnityEngine;

namespace RTD.HexgridEditing.Tiles {
    [CreateAssetMenu(fileName = "T_Prefab_New", menuName = "HexTile/Prefab")]
    public class PrefabTile : HexTile {
        [SerializeField]
        GameObject prefab = default;
        [SerializeField]
        Color editorColor = Color.blue;
        public override Texture GetPreviewTexture() {
            return AssetPreview.GetAssetPreview(prefab.gameObject);
        }

        public override Color GetTelegraphColor() {
            return editorColor;
        }

        public override void PlaceTile(HexMap map, Hex3 position) {
            map.SetHexagonPrefab(position, prefab);
        }
    }
}
