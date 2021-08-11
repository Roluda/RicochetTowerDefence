using RTD.Hexagons;
using RTD.Units.UnitComponents;

namespace RTD.Units.UnitCommands {
    public class MoveOnPath : UnitCommand {
        public override void Execute(Unit unit) {
            if (unit.UnitComponent<Motor>(out var motor) && unit.UnitComponent<Navigator>(out var nav)) {
                if (!nav.Obstructed(nav.Peek())) {
                    motor.onReachTarget += FinishMoving;
                    motor.MoveTo(nav.Dequeue());
                    return;
                }
            }
            Fail();
        }

        protected void FinishMoving(Motor motor) {
            motor.onReachTarget -= FinishMoving;
            Finish();
        }
    }
}
