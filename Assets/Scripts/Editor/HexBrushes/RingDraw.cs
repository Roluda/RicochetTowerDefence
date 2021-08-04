using RTD.Hexagons;
using RTD.Hexgrid;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTD.HexgridEditing.Brushes {
    [CreateAssetMenu(fileName = "HB_RingDraw", menuName = "HexBrush/RingDraw")]
    public class RingDraw : HexBrush {
        Hex3 drawStart = default;

        [SerializeField]
        Texture brushIcon = default;

        public override Texture GetPreviewTexture() {
            return brushIcon;
        }

        public override void DragDraw(HexTile tile, HexMap map, Hex3 position) {
        }

        public override void EndDraw(HexTile tile, HexMap map, Hex3 position) {
            foreach (var hex in HexUtility.HexRing(Hex3.Distance(drawStart, position), drawStart)) {
                tile.PlaceTile(map, hex);
            }
        }

        public override void StartDraw(HexTile tile, HexMap map, Hex3 position) {
            drawStart = position;
        }

        public override IEnumerable<Hex3> GetHexTelegraph(Hex3 currentPosition) {
            return HexUtility.HexRing(Hex3.Distance(drawStart, currentPosition), drawStart);
        }
    }
}