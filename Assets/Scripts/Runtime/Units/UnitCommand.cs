using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units {
    public abstract class UnitCommand : IDisposable {

        public Action onExecutionFinished;
        public Action onExecutionFailed;

        public void Dispose() {
            onExecutionFinished = null;
            onExecutionFailed = null;
        }

        public void Execute(Unit unit, Action onFinish, Action onFail) {
            onExecutionFinished += onFinish;
            onExecutionFailed += onFail;
            Execute(unit);
        }
        public abstract void Execute(Unit unit);
        protected void Finish() {
            onExecutionFinished?.Invoke();
            Dispose();
        }

        protected void Fail() {
            onExecutionFailed?.Invoke();
            Dispose();
        }
    }
}
