using RTD.Hexagons;
using RTD.Hexgrid;
using UnityEditor;
using UnityEngine;

namespace RTD.HexgridEditing.Tiles {
    [CreateAssetMenu(fileName = "T_Delete_New", menuName = "HexTile/Delete")]
    public class DeleteTile : HexTile {
        [SerializeField]
        Texture deleteIcon = default;
        public override Texture GetPreviewTexture() {
            return deleteIcon;
        }

        public override Color GetTelegraphColor() {
            return Color.red;
        }

        public override void PlaceTile(HexMap map, Hex3 position) {
            map.DestroyObject(position);
        }
    }
}
