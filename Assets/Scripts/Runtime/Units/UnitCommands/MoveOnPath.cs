using RTD.Hexagons;
using RTD.Units.UnitComponents;

namespace RTD.Units.UnitCommands {
    public class MoveOnPath : UnitCommand {
        public override void Execute(Unit unit) {
            if (unit.TryGetUnitComponent<Motor>(out var motor) && unit.TryGetUnitComponent<Navigator>(out var nav)) {
                if (!nav.Obstructed(nav.Peek())) {
                    motor.MoveTo(nav.Dequeue());
                }
            }
        }
    }
}
