using RTD.Hexgrid;
using RTD.Progression;
using RTD.Units;
using RTD.Units.UnitCommands;
using RTD.Units.UnitComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTD.Managers {
    public class GameManager : Manager<GameManager> {
        public Action onNewGame = default;
        public Action onStartPlayerTurn = default;
        public Action onStartNpcTurn = default;
        public Action onWin = default;
        public Action onLose = default;

        [Header("Level Settings")]
        [SerializeField]
        int currentLevel = 0;
        [SerializeField]
        LevelPack levelPack = default;

        [Header("Game Settings")]
        [SerializeField]
        int playerActionsPerTurn = 5;

        public int turn { get; private set; }
        public bool isPlayerTurn { get; private set; }

        int currentPlayerActions = 0;

        HexMap currentMap;
        Queue<AI> aiQueue;


        private void Start() {
            LoadCurrentLevel();
        }

        public void PlayerAction() {
            if (!isPlayerTurn) {
                return;
            }
            if (CheckGameOver()) {
                return;
            }
            currentPlayerActions++;
            if(currentPlayerActions >= playerActionsPerTurn) {
                StartNPCTurn();
            }
        }

        public void NextLevel() {
            currentLevel++;
            LoadLevel(currentLevel);
        }

        public void LoadCurrentLevel() {
            LoadLevel(currentLevel);
        }

        public void LoadLevel(int level) {
            if (currentMap) {
                Destroy(currentMap.gameObject);
            }
            currentMap = Instantiate(levelPack.maps[level]);
            NewGame();
        }

        void NewGame() {
            onNewGame?.Invoke();
            turn = 0;
            StartPlayerTurn();
        }

        void StartPlayerTurn() {
            if (!CheckGameOver()) {
                onStartPlayerTurn?.Invoke();
                isPlayerTurn = true;
                currentPlayerActions = 0;
                turn++;
            }
        }

        bool CheckGameOver() {
            if(turn == 0) {
                return false;
            }
            if(UnitRegister.allAI.Count() == 0) {
                onWin?.Invoke();
                NextLevel();
                return true;
            }
            if(UnitRegister.allTowers.Count() == 0) {
                onLose?.Invoke();
                LoadCurrentLevel();
                return true;
            }
            return false;
        }

        void StartNPCTurn() {
            isPlayerTurn = false;
            onStartNpcTurn?.Invoke();
            aiQueue = new Queue<AI>(UnitRegister.allAI.Select(unit => unit.UnitComponent<AI>()).OrderBy(ai => ai.Priority()));
            NextAI();
        }

        void NextAI() {
            if (aiQueue.Count > 0) {
                var ai = aiQueue.Dequeue();
                var turn = new AITurn();
                if (ai && ai.unit.UnitComponent<CommandReceiver>(out var rcv)) {
                    rcv.Execute(turn, NextAI, NextAI);
                } else {
                    NextAI();
                }
            } else {
                StartPlayerTurn();
            }
        }
    }
}
