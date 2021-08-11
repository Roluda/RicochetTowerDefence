using RTD.Hexagons;
using RTD.Managers;
using RTD.Units;
using RTD.Units.UnitCommands;
using RTD.Units.UnitComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTD.PlayerInterface {
    public class UnitController : MonoBehaviour {

        [SerializeField]
        LayerMask playerUnits = default;
        [SerializeField]
        float maxRaycastDistance = 100;
        [SerializeField]
        List<Button> buttons = default;

        bool playerTurn => GameManager.instance.isPlayerTurn;
        Unit currentSelected;

        void Start() {
            foreach (var button in buttons) {
                button.onClick.AddListener(() => MoveSelectedUnit(buttons.IndexOf(button)));
            }
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                var mousePosition = Input.mousePosition;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out var hit, maxRaycastDistance, playerUnits)) {
                    if (hit.collider.TryGetComponent<Unit>(out var unit)) {
                        SelectUnit(unit);
                    }
                }
            }
        }

        void MoveSelectedUnit(int direction) {
            if (currentSelected && currentSelected.UnitComponent<CommandReceiver>(out var rcv)&& playerTurn) {
                var slideCommand = new Slide();
                slideCommand.direction = (Direction)direction;
                rcv.Execute(slideCommand, SlideComplete, SlideComplete);
                DeactivateButtons();
            }
        }

        void SlideComplete() {
            Debug.Log("SLIDE COMPLETED");
            GameManager.instance.PlayerAction();
            ActivateButtons();
        }

        void ActivateButtons() {
            if(!currentSelected.UnitComponent<Navigator>(out var nav)) {
                return;
            }
            foreach (var direction in (Direction[])Enum.GetValues(typeof(Direction))) {
                buttons[(int)direction].gameObject.SetActive(nav.CanMove(direction));
            }
        }

        void DeactivateButtons() {
            buttons.ForEach(button => button.gameObject.SetActive(false));
        }

        void SelectUnit(Unit unit) {
            if (currentSelected && currentSelected.UnitComponent<Skin>(out var skin)) {
                skin.StopHighlight();
            }
            currentSelected = unit;
            if(currentSelected.UnitComponent<Skin>(out skin)) {
                skin.StartHighlight();
            }
            ActivateButtons();
        }
    }
}
