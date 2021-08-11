using RTD.Hexagons;
using RTD.Hexgrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Motor : UnitComponent<MotorSettings> {
        public Action<Motor> onStartMoving = default;
        public Action<Motor> onReachTarget = default;

        HexMap map = default;

        bool moving = false;
        Vector3 start;
        Vector3 target;
        float travelDistance;
        Vector3 direction => (target - start).normalized;

        private void Awake() {
            map = GetComponentInParent<HexMap>();
        }

        public void MoveTo(Vector3 target) {
            start = unit.transform.position;
            this.target = target;
            travelDistance = Vector3.Distance(start, target);
            moving = true;
            onStartMoving?.Invoke(this);
        }

        public void MoveTo(Hex3 target) {
            MoveTo(map.GetWorldPosition(target));
        }

        // Update is called once per frame
        void Update() {
            float distanceToGoal = Vector3.Distance(target, unit.transform.position);
            float distanceFromStart = Vector3.Distance(start, unit.transform.position);
            if (distanceToGoal > settings.arrivalDistance) {
                float progress = Mathf.InverseLerp(travelDistance, 0, distanceToGoal);
                var velocity = direction * Mathf.Lerp(settings.minSpeed, settings.topSpeed, settings.speedInterpolation.Evaluate(progress));
                unit.transform.Translate(velocity * Time.deltaTime);
            }
            if(moving && distanceFromStart >= travelDistance - settings.arrivalDistance) {
                moving = false;
                unit.transform.position = target;
                onReachTarget?.Invoke(this);
            }
        }
    }
}
