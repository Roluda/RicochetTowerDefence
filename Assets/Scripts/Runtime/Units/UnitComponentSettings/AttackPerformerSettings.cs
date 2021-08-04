using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Attack_New", menuName = "UnitComponents/Attack")]
    public class AttackPerformerSettings : UnitComponentSettings {
        [SerializeField]
        public LayerMask hittableLayer = default;
        [SerializeField]
        public float radius = default;
        [SerializeField]
        public float damage = 10;
        [SerializeField]
        public int range = 1;
    }
}
