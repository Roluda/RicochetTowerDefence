using RTD.Hexagons;
using RTD.Hexgrid;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTD.HexgridEditing.Brushes {
    [CreateAssetMenu(fileName = "HB_SimpleDraw_New", menuName = "HexBrush/SimpleDraw")]
    public class SimpleDraw : HexBrush {
        Hex3 lastDrawPosition = default;

        [SerializeField]
        Texture brushIcon = default;

        public override Texture GetPreviewTexture() {
            return brushIcon;
        }

        public override void DragDraw(HexTile tile, HexMap map, Hex3 position) {
            if (position == lastDrawPosition) {
                return;
            }
            tile.PlaceTile(map, position);
            lastDrawPosition = position;
        }

        public override void EndDraw(HexTile tile, HexMap map, Hex3 position) {
        }

        public override void StartDraw(HexTile tile, HexMap map, Hex3 position) {
            tile.PlaceTile(map, position);
            lastDrawPosition = position;
        }

        public override IEnumerable<Hex3> GetHexTelegraph(Hex3 currentPosition) {
            yield return currentPosition;
        }
    }
}
