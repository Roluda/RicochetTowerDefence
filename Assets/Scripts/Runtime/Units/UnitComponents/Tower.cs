using RTD.Hexagons;
using RTD.Hexgrid;
using RTD.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Tower : UnitComponent {
        HexMap map;
        public Hex3 position => map.GetHexPosition(transform.position);

        private void Awake() {
            map = GetComponentInParent<HexMap>();
        }

        protected override void OnEnable() {
            base.OnEnable();
            AIManager.instance.RegisterTower(this);
        }

        protected override void OnDisable() {
            base.OnDisable();
            AIManager.instance.UnregisterTower(this);
        }
    }
}
