using RTD.Hexagons;
using RTD.Hexgrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTD.Units.UnitComponents {
    public class Navigator : UnitComponent<NavigatorSettings> {
        public HexNav nav { get; private set; }
        public HexMap map { get; private set; }

        Queue<Hex3> currentPath = new Queue<Hex3>();
        public Hex3 position => map.GetHexPosition(transform.position);


        public Queue<Hex3> pathPreview => new Queue<Hex3>(currentPath);
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
            if (nav.TryFindPath(position, target, out var path, settings.obstacles)){
                currentPath = new Queue<Hex3>(path);
            } else {
                currentPath = new Queue<Hex3>();
            }
        }

        public Hex3 Slide(Direction direction) {
            if(nav.CanMove(position, direction, settings.obstacles)) {
                return nav.Slide(position, direction, settings.obstacles).Last();
            }
            return position;
        }

        public Hex3 Peek() {
            if (hasPath) {
                return currentPath.Peek();
            }
            return position;
        }

        public Hex3 Dequeue() {
            if (hasPath) {
                return currentPath.Dequeue();
            }
            return position;
        }

        public bool CanMove(Direction direction) {
            return nav.CanMove(position, direction, settings.obstacles);
        }

        public bool Obstructed(Hex3 position) {
            return nav.Obstructed(position, settings.obstacles);
        }
    }
}
