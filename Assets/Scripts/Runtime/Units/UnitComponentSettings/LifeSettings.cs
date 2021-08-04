using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Life_New", menuName = "UnitComponents/Life")]
    public class LifeSettings : UnitComponentSettings {
        [SerializeField]
        public float maxLife = 100;
    }
}
