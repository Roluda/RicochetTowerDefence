using RTD.Hexagons;
using RTD.Hexgrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTD.Units.UnitComponents {
    public class Navigator : UnitComponent {

        [SerializeField]
        NavigatorSettings settings = default;
        [SerializeField]
        LayerMask obstacles = default;

        HexNav nav = default;
        HexMap map = default;

        Queue<Hex3> currentPath = new Queue<Hex3>();
        Hex3 currentPosition => map.GetHexPosition(transform.position);

        public bool hasPath => pathLength > 0;
        public int pathLength => currentPath.Count;

        private void Awake() {
            if (!nav) {
                nav = GetComponentInParent<HexNav>();
            }
            if (!map) {
                map = GetComponentInParent<HexMap>();
            }
        }

        public void CalculatePath(Hex3 target) {
            if (nav.TryFindPath(currentPosition, target, out var path, settings.obstacles)){
                currentPath = new Queue<Hex3>(path);
            } else {
                currentPath = new Queue<Hex3>();
            }
        }

        public Hex3 Slide(Direction direction) {
            if(nav.CanMove(currentPosition, direction)) {
                return nav.Slide(currentPosition, direction).Last();
            }
            return currentPosition;
        }

        public Hex3 Peek() {
            if (hasPath) {
                return currentPath.Peek();
            }
            return currentPosition;
        }

        public Hex3 Dequeue() {
            if (hasPath) {
                return currentPath.Dequeue();
            }
            return currentPosition;
        }

        public bool CanMove(Direction direction) {
            return nav.CanMove(currentPosition, direction, settings.obstacles);
        }

        public bool Obstructed(Hex3 position) {
            return nav.Obstructed(position);
        }
    }
}
