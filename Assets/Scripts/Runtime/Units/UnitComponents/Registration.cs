using RTD.Hexagons;
using RTD.Hexgrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Registration : UnitComponent<RegistrationSettings> {

        protected override void OnEnable() {
            base.OnEnable();
            UnitRegister.Register(unit);
        }
        protected override void OnDisable() {
            base.OnDisable();
            UnitRegister.Unregister(unit);
        }
    }
}
