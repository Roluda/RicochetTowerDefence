using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Hitbox : UnitComponent<HitboxSettings> {

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent<Unit>(out var enemy)) {
                if(enemy.UnitComponent<Hurtbox>(out var hurtbox)) {
                    HandleHit(hurtbox);
                }
            }
        }

        void HandleHit(Hurtbox hurtbox) {
            if(hurtbox.unit.UnitComponent<Life>(out var life)) {
                life.TakeDamage(settings.collisionDamage);
            }
        }
    }
}
