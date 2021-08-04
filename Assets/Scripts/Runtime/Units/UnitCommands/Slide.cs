using RTD.Hexagons;
using RTD.Units.UnitComponents;

namespace RTD.Units.UnitCommands {
    public class Slide : UnitCommand {
        public Direction direction;
        public override void Execute(Unit unit) {
            if (unit.TryGetUnitComponent<Motor>(out var motor) && unit.TryGetUnitComponent<Navigator>(out var nav)) {
                motor.MoveTo(nav.Slide(direction));
            }
        }
    }
}

