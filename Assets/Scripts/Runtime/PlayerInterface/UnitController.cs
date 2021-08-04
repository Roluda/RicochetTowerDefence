using RTD.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.PlayerInterface {
    public class UnitController : MonoBehaviour {

        [SerializeField]
        LayerMask playerUnits = default;
        [SerializeField]
        float maxRaycastDistance = 100;

        bool playerTurn;
        Unit currentSelected;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
