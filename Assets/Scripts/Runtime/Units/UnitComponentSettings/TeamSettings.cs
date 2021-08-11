using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Team_New", menuName = "UnitComponents/Team")]
    public class TeamSettings : UnitComponentSettings {
        [SerializeField, Layer]
        public int teamLayer = default;
        [SerializeField]
        public TeamMembership membership = default;
    }
}
