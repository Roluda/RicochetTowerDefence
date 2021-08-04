using RTD.Hexagons;
using RTD.Hexgrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    public class Motor : UnitComponent {
        public Action<Motor> onStartMoving = default;
        public Action<Motor> onReachTarget = default;

        [SerializeField]
        MotorSettings settings = default;
        [SerializeField]
        Rigidbody unitRigidbody = default;

        HexMap map = default;

        bool moving = false;
        Vector3 start;
        Vector3 target;
        float travelDistance;
        Vector3 direction => (target - start).normalized;

        protected override void OnValidate() {
            base.OnValidate();
            unitRigidbody = GetComponentInParent<Rigidbody>();
            unitRigidbody.isKinematic = true;
        }

        public void MoveTo(Vector3 target) {
            start = unitRigidbody.position;
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
            float distanceToGoal = Vector3.Distance(target, unitRigidbody.position);
            if (distanceToGoal > settings.arrivalDistance) {
                float progress = Mathf.InverseLerp(travelDistance, 0, distanceToGoal);
                unitRigidbody.velocity = direction * Mathf.Lerp(settings.minSpeed, settings.topSpeed, settings.speedInterpolation.Evaluate(progress));
            } else if(moving) {
                moving = false;
                unitRigidbody.velocity = Vector3.zero;
                onReachTarget?.Invoke(this);
            }
        }
    }
}
