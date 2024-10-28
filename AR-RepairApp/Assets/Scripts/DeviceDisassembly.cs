using UnityEngine;
using UnityEngine.UI;

public class DeviceDisassembly : MonoBehaviour
{
    public enum DisassemblyState
    {
        Idle,
        ScrewsOut,
        CapOff,
        BatteryOut,
        RamOut,
        SSDOut
    }

    public DisassemblyState currentState = DisassemblyState.Idle;

    public Button leftButton;
    public Button rightButton;
    public Animator animator;

    void Start()
    {
        leftButton.onClick.AddListener(() => MoveToPreviousState());
        rightButton.onClick.AddListener(() => MoveToNextState());
        
        UpdateButtons();
    }

    void MoveToNextState()
    {
        switch (currentState)
        {
            case DisassemblyState.Idle:
                ChangeState(DisassemblyState.ScrewsOut, "ScrewsOut");
                break;

            case DisassemblyState.ScrewsOut:
                ChangeState(DisassemblyState.CapOff, "CapOff");
                break;

            case DisassemblyState.CapOff:
                ChangeState(DisassemblyState.BatteryOut, "BatteryOut");
                break;

            case DisassemblyState.BatteryOut:
                ChangeState(DisassemblyState.RamOut, "RamOut");
                break;

            case DisassemblyState.RamOut:
                ChangeState(DisassemblyState.SSDOut, "SsdOut");
                break;

            case DisassemblyState.SSDOut:
                // Optional: Loop back to Idle or disable the right button.
                break;
        }
    }

    void MoveToPreviousState()
    {
        switch (currentState)
        {
            case DisassemblyState.ScrewsOut:
                ChangeState(DisassemblyState.Idle, "ScrewsIn");
                break;

            case DisassemblyState.CapOff:
                ChangeState(DisassemblyState.ScrewsOut, "CapIn");
                break;

            case DisassemblyState.BatteryOut:
                ChangeState(DisassemblyState.CapOff, "BatteryIn");
                break;

            case DisassemblyState.RamOut:
                ChangeState(DisassemblyState.BatteryOut, "RamIn");
                break;

            case DisassemblyState.SSDOut:
                ChangeState(DisassemblyState.RamOut, "SsdIn");
                break;

            case DisassemblyState.Idle:
                // Optional: Loop back to SSDOut or disable the left button.
                break;
        }
    }

    void ChangeState(DisassemblyState newState, string trigger)
    {
        currentState = newState;
        animator.SetTrigger(trigger);
        UpdateButtons();
    }

    // Function to update the buttons' states based on the current phase
    void UpdateButtons()
    {
        // Enable or disable left/right buttons based on the current state
        leftButton.interactable = currentState != DisassemblyState.Idle;
        rightButton.interactable = currentState != DisassemblyState.SSDOut;
    }
}
