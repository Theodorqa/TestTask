using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

namespace FSM
{
    public class InitializeFSM : MonoBehaviourExtBind
    {
        [OnAwake]
        private void InitFsm()
        {
            Settings.Fsm = new AxGrid.FSM.FSM();
            
            var startState = new StartScrollState();
            Settings.Fsm.Add(startState);

            var stopState = new StopScrollState();
            Settings.Fsm.Add(stopState);
            
            Log.Debug("FSM Initialize");
        }
        
        
        [OnUpdate]
        private void UpdateThis()
        {
            Settings.Fsm.Update(Time.deltaTime);
        }
        
        [Bind("ButtonToFSM")]
        public void OnBtn(string buttonName)
        {
            Log.Debug("Была нажата кнопка " + buttonName);

            if (buttonName == "Stop")
            {
                Settings.Fsm.Change("StopScrollState");
            }
            else
            {
                Settings.Fsm.Change("StartScrollState");
                Settings.Model.EventManager.Invoke("OnStartButtonClick");
                
            }
        }

    }
}