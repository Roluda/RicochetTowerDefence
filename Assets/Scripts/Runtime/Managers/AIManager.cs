using RTD.Units;
using RTD.Units.UnitCommands;
using RTD.Units.UnitComponents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTD.Managers {
    public class AIManager : Manager<AIManager> {
        [SerializeField]
        float commandInterval = 1;

        List<Unit> agents = new List<Unit>();
        List<Tower> towers = new List<Tower>();

        private void Start() {
            TurnManager.instance.onStartNpcTurn += HandleAI;
        }

        public void RegisterTower(Tower unit) {
            towers.Add(unit);
        }

        public void UnregisterTower(Tower unit) {
            towers.Remove( unit);
        }

        public void RegisterAgent(Unit unit) {
            agents.Add(unit);
        }

        public void UnregisterAgent(Unit unit) {
            agents.Remove(unit);
        }

        void HandleAI() {
            agents.OrderBy(agent => AgentPriority(agent));
        }

        float AgentPriority(Unit unit) {
            if(unit.TryGetUnitComponent<Navigator>(out var nav)) {
                if (nav.hasPath) {
                    return nav.pathLength;
                }
            }
            return float.MaxValue;
        }

        IEnumerator SendCommands(IEnumerable<Unit> units) {
            yield return new WaitForSeconds(commandInterval);
            foreach (var unit in units) {
                SendCommand(unit);
                yield return new WaitForSeconds(commandInterval);
            }
            TurnManager.instance.EndNpcTurn();
        }

        void SendCommand(Unit unit) {
            if (unit.TryGetUnitComponent<Navigator>(out var nav) && unit.TryGetUnitComponent<CommandReceiver>(out var rcv)) {
                if (!nav.hasPath && towers.Count > 0) {
                    var pathCommand = new CalculatePath();
                    pathCommand.target = towers.First().position;
                    rcv.Execute(pathCommand);
                }
                if (nav.hasPath) {
                    rcv.Execute(new MoveOnPath());
                }
            }
        }
    }
}
