using RTD.Hexagons;
using RTD.Hexgrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class AttackPerformer : UnitComponent<AttackPerformerSettings> {
        HexMap map;

        Hex3 position => map.GetHexPosition(transform.position);

        public int range => settings.range;
        public float radius => settings.radius;

        TeamMembership selfMembership;
        private void Awake() {
            if (!map) {
                map = GetComponentInParent<HexMap>();
            }
        }
        private void Start() {
            if(unit.UnitComponent<Team>(out var team)) {
                selfMembership = team.membership;
            }
        }

        public void Attack(Hex3 target) {
            if(Hex3.Distance(target, position) > settings.range) {
                return;
            }
            var colliders = Physics.OverlapSphere(map.GetWorldPosition(target), settings.radius, settings.hittableLayer);
            foreach(var col in colliders) {
                if(col.TryGetComponent<Unit>(out var unit)) {
                    if (unit.UnitComponent<Hurtbox>(out var hurtbox)) {
                        HandleHit(hurtbox);
                    }
                }
            }
        }

        void HandleHit(Hurtbox hurtbox) {
            if (hurtbox.unit.UnitComponent<Team>(out var team) && team.IsFoe(selfMembership)) {
                Debug.Log("Foe");
                if (hurtbox.unit.UnitComponent<Life>(out var life)) {
                    Debug.Log("Life");
                    life.TakeDamage(settings.damage);
                }
            }
        }
    }
}
