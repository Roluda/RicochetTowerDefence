using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Team : UnitComponent<TeamSettings> {
        public TeamMembership membership => settings.membership;

        public bool IsFoe(TeamMembership comparement) {
            return comparement != membership;
        }

        private void Awake() {
            unit.gameObject.layer = settings.teamLayer;
        }
    }
}
