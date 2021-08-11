using RTD.Hexagons;
using RTD.Managers;
using RTD.Units.UnitCommands;
using System;
using System.Linq;

namespace RTD.Units.UnitComponents {
    public class AI : UnitComponent<AISettings> {

        public Action<AI> onStartTurn;
        public Action<AI> onFinishTurn;

        public float Priority() {
            if (unit.UnitComponent<Navigator>(out var nav)) {
                if (nav.hasPath) {
                    return nav.pathLength;
                }
            }
            return float.MaxValue;
        }

        public void DoTurn() {
            if (unit.UnitComponent<Navigator>(out var nav)) {
                var tower = UnitRegister.allTowers.Select(unit  => unit.UnitComponent<Tower>()).FirstOrDefault();
                if (!tower) {
                    FinishTurn();
                    return;
                }
                if (unit.UnitComponent<AttackPerformer>(out var performer) && Hex3.Distance(tower.position, nav.position) <= performer.range) {
                    var attackCommand = new Attack();
                    attackCommand.target = tower.position;
                    attackCommand.Execute(unit, FinishTurn, FinishTurn);
                }
                if (!nav.hasPath) {
                    var pathCommand = new CalculatePath();
                    pathCommand.target = tower.ClosestPossibleAttackPoint(nav.position);
                    pathCommand.Execute(unit);
                }
                if (nav.hasPath) {
                    var moveOnPath = new MoveOnPath();
                    moveOnPath.Execute(unit, FinishTurn, FinishTurn);
                    return;
                }
                FinishTurn();
            } else {
                FinishTurn();
            }
        }

        void FinishTurn() {
            onFinishTurn?.Invoke(this);
        }
    }
}
