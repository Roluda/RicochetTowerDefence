using RTD.Hexagons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Hexgrid {
    public class HexNavAgent : MonoBehaviour {
        HexMap map;
        HexNav nav;

        public Hex3 hexPosition => map.GetHexPosition(transform.position);

        private void Awake() {
            map = GetComponentInParent<HexMap>();
            nav = GetComponentInParent<HexNav>();
        }

        private void OnEnable() {
            nav.RegisterAgent(this);
        }

        private void OnDisable() {
            nav.UnregisterAgent(this);
        }
    }
}
