using RTD.Hexagons;
using RTD.Units.UnitComponents;
using System;

namespace RTD.Units.UnitCommands {
    public class MoveTo : UnitCommand {
        public Hex3 target;
        public override void Execute(Unit unit) {
            if (unit.UnitComponent<Motor>(out var motor)) {
                motor.onReachTarget += FinishMoving;
                motor.MoveTo(target);
            } else {
                Fail();
            }
        }

        private void FinishMoving(Motor motor) {
            motor.onReachTarget -= FinishMoving;
            Finish();
        }
    }
}
