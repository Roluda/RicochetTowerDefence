using RTD.Hexagons;
using RTD.Hexgrid;
using UnityEngine;

namespace RTD.HexgridEditing {
    public abstract class HexTile : ScriptableObject {
        public abstract Color GetTelegraphColor();
        public abstract Texture GetPreviewTexture();
        public abstract void PlaceTile(HexMap map, Hex3 position);
    }
}
