using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Tower_New", menuName = "UnitComponents/Tower")]
    public class TowerSettings : UnitComponentSettings {
        public int maxAttackRadius = 2;
    }
}
