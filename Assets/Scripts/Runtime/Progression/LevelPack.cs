using RTD.Hexgrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Progression {
    [CreateAssetMenu(fileName = "LP_New", menuName = "Progression/LevelPack" )]
    public class LevelPack : ScriptableObject {
        [SerializeField]
        public HexMap[] maps = default;
    }
}
