using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units {
    public abstract class UnitCommand {
        public abstract void Execute(Unit unit);
    }
}
