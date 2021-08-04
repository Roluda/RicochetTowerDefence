namespace RTD.Units.UnitComponents {
    public class CommandReceiver : UnitComponent {
        public void Execute(UnitCommand command) {
            command.Execute(unit);
        }
    }
}
