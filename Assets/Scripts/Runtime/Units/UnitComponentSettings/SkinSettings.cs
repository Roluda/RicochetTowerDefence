using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units.UnitComponents {
    [CreateAssetMenu(fileName = "UCS_Skin_New", menuName = "UnitComponents/Skin")]
    public class SkinSettings : UnitComponentSettings {
        [SerializeField, Layer]
        public int outlineLayer = default;
        [SerializeField, Layer]
        public int defaultLayer = default;
        [SerializeField]
        public Renderer skinPrefab = default;
    }
}
