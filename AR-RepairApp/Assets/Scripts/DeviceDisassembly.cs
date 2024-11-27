using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

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

    // Prefabs for each state did this because im done with the animation controller and it sort of works but not really so im about to make the whole project dependant on a coconut picture
    public GameObject idlePrefab;
    public GameObject screwsOutPrefab;
    public GameObject capOffPrefab;
    public GameObject batteryOutPrefab;
    public GameObject ramOutPrefab;
    public GameObject ssdOutPrefab;

    public ARTrackedImageManager arTrackedImageManager;

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
                ChangeState(DisassemblyState.ScrewsOut, screwsOutPrefab);
                break;

            case DisassemblyState.ScrewsOut:
                ChangeState(DisassemblyState.CapOff, capOffPrefab);
                break;

            case DisassemblyState.CapOff:
                ChangeState(DisassemblyState.BatteryOut, batteryOutPrefab);
                break;

            case DisassemblyState.BatteryOut:
                ChangeState(DisassemblyState.RamOut, ramOutPrefab);
                break;

            case DisassemblyState.RamOut:
                ChangeState(DisassemblyState.SSDOut, ssdOutPrefab);
                break;

            case DisassemblyState.SSDOut:
                break;
        }
    }

    void MoveToPreviousState()
    {
        switch (currentState)
        {
            case DisassemblyState.ScrewsOut:
                ChangeState(DisassemblyState.Idle, idlePrefab);
                break;

            case DisassemblyState.CapOff:
                ChangeState(DisassemblyState.ScrewsOut, screwsOutPrefab);
                break;

            case DisassemblyState.BatteryOut:
                ChangeState(DisassemblyState.CapOff, capOffPrefab);
                break;

            case DisassemblyState.RamOut:
                ChangeState(DisassemblyState.BatteryOut, batteryOutPrefab);
                break;

            case DisassemblyState.SSDOut:
                ChangeState(DisassemblyState.RamOut, ramOutPrefab);
                break;

            case DisassemblyState.Idle:
                break;
        }
    }

    void ChangeState(DisassemblyState newState, GameObject newPrefab)
    {
        currentState = newState;
        ReplaceTrackedImagePrefab(newPrefab);
        UpdateButtons();
    }


    void ReplaceTrackedImagePrefab(GameObject newPrefab)
{
    foreach (var trackedImage in arTrackedImageManager.trackables)
    {
        foreach (Transform child in trackedImage.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newInstance = Instantiate(newPrefab, trackedImage.transform);

        HoverOffset hoverOffset = newInstance.AddComponent<HoverOffset>();
        hoverOffset.hoverHeight = 0.1f; 
    }
}


    void UpdateButtons()
    {
        leftButton.interactable = currentState != DisassemblyState.Idle;
        rightButton.interactable = currentState != DisassemblyState.SSDOut;
    }
}
