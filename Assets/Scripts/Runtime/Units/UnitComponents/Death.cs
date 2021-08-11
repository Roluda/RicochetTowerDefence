using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Death : UnitComponent<DeathSettings> {

        public Action<Death> onDeath;

        private void Start() {
            if(unit.UnitComponent<Life>(out var life)){
                life.onDamageTaken += HandleDamageTaken;
            }
        }

        void HandleDamageTaken(Life life, float damage) {
            if (life.life == 0) {
                onDeath?.Invoke(this);
                Destroy(unit.gameObject);
            }
        }
    }
}
