using RTD.Hexagons;
using RTD.Hexgrid;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.HexgridEditing {
    public abstract class HexBrush : ScriptableObject {
        public abstract Texture GetPreviewTexture();
        public abstract IEnumerable<Hex3> GetHexTelegraph(Hex3 currentPosition);
        public abstract void StartDraw(HexTile tile, HexMap map, Hex3 position);
        public abstract void DragDraw(HexTile tile, HexMap map, Hex3 position);
        public abstract void EndDraw(HexTile tile, HexMap map, Hex3 position);
    }
}
