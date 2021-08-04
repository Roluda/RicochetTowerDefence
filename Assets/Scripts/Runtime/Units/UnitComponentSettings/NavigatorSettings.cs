using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Navigator_New", menuName = "UnitComponents/Navigator")]
    public class NavigatorSettings : UnitComponentSettings {
        [SerializeField]
        public LayerMask obstacles = default;
    }
}