using RTD.Units.UnitComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitCommands {
    public class AITurn : UnitCommand {
        public override void Execute(Unit unit) {
            if (unit.UnitComponent<AI>(out var ai)) {
                ai.onFinishTurn += FinishTurn;
                ai.DoTurn();
            } else {
                Fail();
            }
        }

        protected void FinishTurn(AI ai) {
            ai.onFinishTurn -= FinishTurn;
            Finish();
        }
    }
}
