using RTD.Hexagons;
using RTD.Units.UnitComponents;

namespace RTD.Units.UnitCommands {
    public class CalculatePath : UnitCommand {
        public Hex3 target;
        public override void Execute(Unit unit) {
            if (unit.UnitComponent<Navigator>(out var nav)) {
                nav.CalculatePath(target);
                Finish();
            } else {
                Fail();
            }
        }
    }
}

