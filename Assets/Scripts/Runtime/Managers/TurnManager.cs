using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Managers {
    public class TurnManager : Manager<TurnManager> {
        public int turn { get; private set; }

        public Action onNewGame = default;
        public Action onStartPlayerTurn = default;
        public Action onStartNpcTurn = default;

        public bool isPlayerTurn { get; private set; }

        public void NewGame() {
            onNewGame?.Invoke();
            onStartPlayerTurn?.Invoke();
            isPlayerTurn = true;
            turn = 0;
        }

        public void EndPlayerTurn() {
            isPlayerTurn = false;
            onStartNpcTurn?.Invoke();
        }

        public void EndNpcTurn() {
            isPlayerTurn = true;
            onStartPlayerTurn?.Invoke();
            turn++;
        }
    }
}
