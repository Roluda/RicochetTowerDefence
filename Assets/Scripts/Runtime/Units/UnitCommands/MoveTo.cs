using RTD.Hexagons;
using RTD.Units.UnitComponents;

namespace RTD.Units.UnitCommands {
    public class MoveTo : UnitCommand {
        public Hex3 target;
        public override void Execute(Unit unit) {
            if (unit.TryGetUnitComponent<Motor>(out var motor)) {
                motor.MoveTo(target);
            }
        }
    }
}
