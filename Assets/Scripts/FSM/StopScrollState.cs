using AxGrid;
using AxGrid.FSM;

namespace FSM
{
    [State("StopScrollState")]
    public class StopScrollState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("StopScrollState: Enter");
            VisualPartLootbox.Instance.StopScrolling();
        }

        [Exit]
        public void Exit()
        {
            Log.Debug("StopScrollState: Exit");
        }
    }
}