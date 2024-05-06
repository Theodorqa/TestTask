using AxGrid;
using AxGrid.FSM;

namespace FSM
{
    [State("StartScrollState")]
    public class StartScrollState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("StartScrollState: Enter");
            VisualPartLootbox.Instance.StartScrolling();
        }

        [Exit]
        public void Exit()
        {
            Log.Debug("StartScrollState: Exit");
        }
    }
}