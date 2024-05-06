using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Tools.Binders;
using UnityEngine;

public class ButtonsLootboxController : MonoBehaviourExtBind
{
    [SerializeField] private UIButtonDataBind startButton;
    [SerializeField] private UIButtonDataBind stopButton;

    private bool startButtonClicked = false;
    private float startButtonClickedTime;

    [OnStart]
    private void StartThis()
    {
        startButtonClicked = false;
    }

    [OnUpdate]
    private void UpdateThis()
    {
        if (startButtonClicked && Time.time - startButtonClickedTime < 3f)
        {
            stopButton.CancelClick();
            startButton.CancelClick();
        }
    }

    [Bind("OnStartButtonClick")]
    public void OnStartButtonClick()
    {
        startButtonClicked = true;
        startButtonClickedTime = Time.time;
        
        startButton.defaultEnable = false;
        Invoke("UnlockStartButton", 3f);
    }

    private void UnlockStartButton()
    {
        startButton.defaultEnable = true;
    }
}