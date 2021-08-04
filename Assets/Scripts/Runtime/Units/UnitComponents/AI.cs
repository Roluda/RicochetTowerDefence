using RTD.Managers;

namespace RTD.Units {
    public class AI : UnitComponent {
        protected override void OnEnable() {
            base.OnEnable();
            AIManager.instance.RegisterAgent(unit);
        }

        protected override void OnDisable() {
            base.OnDisable();
            AIManager.instance.UnregisterAgent(unit);
        }
    }
}
