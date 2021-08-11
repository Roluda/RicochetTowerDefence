using RTD.Hexagons;
using RTD.Units.UnitComponents;
using System;

namespace RTD.Units.UnitCommands {
    public class Slide : UnitCommand {
        public Direction direction;
        public override void Execute(Unit unit) {
            if (unit.UnitComponent<Motor>(out var motor) && unit.UnitComponent<Navigator>(out var nav)) {
                motor.onReachTarget += FinishMoving;
                motor.MoveTo(nav.Slide(direction));
                return;
            }
            Fail();
        }

        private void FinishMoving(Motor motor) {
            motor.onReachTarget -= FinishMoving;
            Finish();
        }
    }
}

