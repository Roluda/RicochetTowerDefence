using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Hitbox_New", menuName = "UnitComponents/Hitbox")]
    public class HitboxSettings : UnitComponentSettings {
        [SerializeField]
        public float collisionDamage = 10;
    }
}
