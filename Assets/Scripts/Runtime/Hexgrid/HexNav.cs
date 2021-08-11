using RTD.Hexagons;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Hexgrid {
    public class HexNav : MonoBehaviour {
        [SerializeField]
        HexMap hexMap = default;
        [SerializeField]
        LayerMask obstacles = default;

        LayerMask lastUsedLayer;

        IEnumerable<Hex3> walkableCache;

        List<HexNavAgent> agents = new List<HexNavAgent>();

        private void OnValidate() {
            if (!hexMap) {
                hexMap = GetComponentInParent<HexMap>();
            }
        }

        public IEnumerable<Hex3> Slide(Hex3 start, Direction direction) {
            return Slide(start, direction, obstacles);
        }

        public IEnumerable<Hex3> Slide(Hex3 start, Direction direction, LayerMask obstacles) {
            var nextStep = start;
            while(CanMove(nextStep, direction, obstacles)) {
                nextStep = nextStep.Neighbor(direction);
                yield return nextStep;
            }
        }

        public bool Obstructed(Hex3 test) {
            return Obstructed(test, obstacles);
        }

        public bool Obstructed(Hex3 test, LayerMask obstacles) {
            return !hexMap.TryGetObject(test, out var gameObject)
                || IsOnLayer(gameObject.layer, obstacles)
                || agents.Where(agent => IsOnLayer(agent.gameObject.layer, obstacles)).Any(agent => agent.hexPosition == test);
        }

        public bool CanMove(Hex3 start, Direction direction) {
            return CanMove(start, direction, obstacles);
        }

        public bool CanMove(Hex3 start, Direction direction, LayerMask obstacles) {
            return !Obstructed(start.Neighbor(direction), obstacles);
        }

        public bool TryFindPath(Hex3 start, Hex3 goal, out IEnumerable<Hex3> path) {
            return TryFindPath(start, goal, out path, obstacles);
        }

        public bool TryFindPath(Hex3 start, Hex3 goal, out IEnumerable<Hex3> path, LayerMask obstacles) {
            if(walkableCache == null || lastUsedLayer != obstacles) {
                lastUsedLayer = obstacles;
                walkableCache = hexMap.GetAllObjects().Where((tile) => !IsOnLayer(tile.Item2.layer, obstacles)).Select(tile => tile.Item1);
            }
            var availableHexes = walkableCache
                .Except(agents.Where(agent => IsOnLayer(agent.gameObject.layer, obstacles) && agent.hexPosition != start)
                .Select(agent => agent.hexPosition));
            return HexUtility.TryFindPath(start, goal, availableHexes, out path);
        } 

        bool IsOnLayer(int layer, LayerMask obstructed) {
            return (obstructed & (1<<layer)) == (1<<layer);
        }

        public void RegisterAgent(HexNavAgent agent) {
            agents.Add(agent);
        }

        public void UnregisterAgent(HexNavAgent agent) {
            agents.Remove(agent);
        }
    }
}
