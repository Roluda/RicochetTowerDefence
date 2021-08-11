using RTD.Hexagons;
using RTD.Hexgrid;
using RTD.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTD.Units.UnitComponents {
    public class Tower : UnitComponent<TowerSettings> {
        HexMap map;
        HexNav nav;
        public Hex3 position => map.GetHexPosition(transform.position);

        public Hex3 ClosestPossibleAttackPoint(Hex3 start) {
            for (int i = 1; i <= settings.maxAttackRadius; i++) {
                var neighbors = HexUtility.HexRing(i, position).Where(hex => !nav.Obstructed(hex));
                if(neighbors.Count() > 0) {
                    return neighbors.OrderBy(hex => Hex3.Distance(hex, start)).First();
                }
            }
            return position;
        }

        private void Awake() {
            map = GetComponentInParent<HexMap>();
            nav = GetComponentInParent<HexNav>();
        }
    }
}
