using RTD.Units.UnitComponents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTD.Units {
    public static class UnitRegister{
        static List<Unit> registeredUnits = new List<Unit>();

        public static void Register(Unit unit) {
            registeredUnits.Add(unit);
        }

        public static void Unregister(Unit unit) {
            registeredUnits.Remove(unit);
        }

        public static IEnumerable<Unit> allUnits => registeredUnits;

        public static IEnumerable<Unit> allTowers => registeredUnits.Where(unit => unit.UnitComponent<Tower>());

        public static IEnumerable<Unit> allAI => registeredUnits.Where(unit => unit.UnitComponent<AI>());
    }
}
