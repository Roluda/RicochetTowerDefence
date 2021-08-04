using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Motor_New", menuName ="UnitComponents/Motor")]
    public class MotorSettings : UnitComponentSettings {
        [SerializeField]
        public float topSpeed = 10;
        [SerializeField]
        public float minSpeed = 1;
        [SerializeField]
        public AnimationCurve speedInterpolation = default;
        [SerializeField]
        public float arrivalDistance = 0.05f;
    }
}
