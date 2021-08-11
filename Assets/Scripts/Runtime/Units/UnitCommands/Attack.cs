using RTD.Hexagons;
using RTD.Units.UnitComponents;

namespace RTD.Units.UnitCommands {
    public class Attack : UnitCommand {
        public Hex3 target;
        public override void Execute(Unit unit) {
            if (unit.UnitComponent<AttackPerformer>(out var attack)) {
                attack.Attack(target);
                Finish();
            } else {
                Fail();
            }
        }
    }
}
