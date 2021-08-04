using RTD.Hexagons;
using RTD.Hexgrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class AttackPerformer : UnitComponent {
        HexMap map;

        Hex3 position => map.GetHexPosition(transform.position);

        [SerializeField]
        AttackPerformerSettings settings;

        private void Awake() {
            if (!map) {
                map = GetComponentInParent<HexMap>();
            }
        }

        public void Attack(Hex3 target) {
            if(Hex3.Distance(target, position) > settings.range) {
                return;
            }
            var colliders = Physics.OverlapSphere(map.GetWorldPosition(target), settings.radius, settings.hittableLayer);
            foreach(var col in colliders) {
                if(col.TryGetComponent<Unit>(out var unit)) {
                    if(unit.TryGetUnitComponent<Hurtbox>(out var hurtbox)) {
                        HandleHit(hurtbox);
                    }
                }
            }
        }

        void HandleHit(Hurtbox hurtbox) {
            if (hurtbox.unit.TryGetUnitComponent<Life>(out var life)) {
                life.TakeDamage(settings.damage);
            }
        }
    }
}
