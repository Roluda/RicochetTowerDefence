using System;

namespace RTD.Units.UnitComponents {
    public class CommandReceiver : UnitComponent<CommandReceiverSettings> {
        public void Execute(UnitCommand command) {
            command.Execute(unit);
        }

        public void Execute(UnitCommand command, Action onFinish, Action onFail) {
            command.Execute(unit, onFinish, onFail);
        }
    }
}
