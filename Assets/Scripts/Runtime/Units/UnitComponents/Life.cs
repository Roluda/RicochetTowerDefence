using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTD.Units.UnitComponents {
    public class Life : UnitComponent {
        [SerializeField]
        LifeSettings settings = default;
        float currentLife = 0;

        public float life => currentLife;
        public float normalizedLife => life / settings.maxLife;

        public float maxLife => settings.maxLife;

        private void Start() {
            currentLife = settings.maxLife;
        }

        public void TakeDamage(float damage) {
            currentLife = Mathf.Max(0, currentLife -damage);
        }

        public void Heal(float healing) {
            currentLife = Mathf.Min(maxLife, currentLife + healing);
        }


    }
}
