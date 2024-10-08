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

    public Button idleButton;
    public Button screwsOutButton;
    public Button capOffButton;
    public Button batteryOutButton;
    public Button ramOutButton;
    public Button ssdOutButton;

    void Start()
    {
        idleButton.onClick.AddListener(() => ChangeState(DisassemblyState.ScrewsOut));
        screwsOutButton.onClick.AddListener(() => ChangeState(DisassemblyState.CapOff));
        capOffButton.onClick.AddListener(() => ChangeState(DisassemblyState.CapOff));

        batteryOutButton.onClick.AddListener(() => ChangeState(DisassemblyState.BatteryOut));
        ramOutButton.onClick.AddListener(() => ChangeState(DisassemblyState.RamOut));
        ssdOutButton.onClick.AddListener(() => ChangeState(DisassemblyState.SSDOut));

        SetInitialButtonState();
    }

    void SetInitialButtonState()
    {
        idleButton.interactable = true;
        screwsOutButton.interactable = false;
        capOffButton.interactable = false;

        batteryOutButton.gameObject.SetActive(false);
        ramOutButton.gameObject.SetActive(false);
        ssdOutButton.gameObject.SetActive(false);
    }

    void ChangeState(DisassemblyState newState)
    {
        switch (currentState)
        {
            case DisassemblyState.Idle:
                if (newState == DisassemblyState.ScrewsOut)
                {
                    currentState = DisassemblyState.ScrewsOut;
                    OnEnterScrewsOut();
                    UpdateButtons();
                }
                break;

            case DisassemblyState.ScrewsOut:
                if (newState == DisassemblyState.CapOff || newState == DisassemblyState.Idle)
                {
                    currentState = newState;
                    if (newState == DisassemblyState.CapOff)
                    {
                        OnEnterCapOff();
                    }
                    else
                    {
                        OnEnterIdle();
                    }
                    UpdateButtons();
                }
                break;

            case DisassemblyState.CapOff:
                if (newState == DisassemblyState.ScrewsOut)
                {
                    currentState = DisassemblyState.ScrewsOut;
                    OnEnterScrewsOut();
                    UpdateButtons();
                }
                else if (newState == DisassemblyState.BatteryOut || newState == DisassemblyState.RamOut || newState == DisassemblyState.SSDOut)
                {
                    currentState = newState;
                    if (newState == DisassemblyState.BatteryOut) OnEnterBatteryOut();
                    if (newState == DisassemblyState.RamOut) OnEnterRamOut();
                    if (newState == DisassemblyState.SSDOut) OnEnterSSDOut();
                    UpdateButtons();
                }
                break;

            case DisassemblyState.BatteryOut:
            case DisassemblyState.RamOut:
            case DisassemblyState.SSDOut:
                if (newState == DisassemblyState.CapOff)
                {
                    currentState = DisassemblyState.CapOff;
                    OnEnterCapOff();
                    UpdateButtons();
                }
                break;
        }
    }

    // Function to update the buttons' states based on the current phase
    void UpdateButtons()
    {
        idleButton.interactable = false;
        screwsOutButton.interactable = false;
        capOffButton.interactable = false;
        batteryOutButton.interactable = false;
        ramOutButton.interactable = false;
        ssdOutButton.interactable = false;

        switch (currentState)
        {
            case DisassemblyState.Idle:
                idleButton.interactable = true;
                screwsOutButton.interactable = false;
                capOffButton.interactable = false;
                break;

            case DisassemblyState.ScrewsOut:
                idleButton.interactable = true;
                screwsOutButton.interactable = true;
                capOffButton.interactable = false;
                break;

            case DisassemblyState.CapOff:
                screwsOutButton.interactable = true;
                capOffButton.interactable = true;
                
                // Show and enable the component buttons (Battery, RAM, SSD)
                batteryOutButton.gameObject.SetActive(true);
                ramOutButton.gameObject.SetActive(true);
                ssdOutButton.gameObject.SetActive(true);

                batteryOutButton.interactable = true;
                ramOutButton.interactable = true;
                ssdOutButton.interactable = true;
                break;

            case DisassemblyState.BatteryOut:
            case DisassemblyState.RamOut:
            case DisassemblyState.SSDOut:
                capOffButton.interactable = true;
                break;
        }
    }

    void OnEnterIdle()
    {
        Debug.Log("Entered Idle state");
    }

    void OnEnterScrewsOut()
    {
        Debug.Log("Entered Screws Out state");
    }

    void OnEnterCapOff()
    {
        Debug.Log("Entered Cap Off state");
    }

    void OnEnterBatteryOut()
    {
        Debug.Log("Entered Battery Out state");
    }

    void OnEnterRamOut()
    {
        Debug.Log("Entered RAM Out state");
    }

    void OnEnterSSDOut()
    {
        Debug.Log("Entered SSD Out state");
    }
}
