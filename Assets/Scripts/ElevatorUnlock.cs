using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ElevatorUnlock : MonoBehaviour
{
    [Header("Door Settings")]
    public GameObject elevatorDoor;
    public GameObject elevatorBox;

    [Header("Visual Feedback")]
    public MeshRenderer indicatorLight;
    public Material greenMaterial;

    private XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();

        // Listen for when the keycard is successfully slotted
        socket.selectEntered.AddListener(OnKeycardInserted);
    }
    private void OnKeycardInserted(SelectEnterEventArgs args)
    {
        // Fallback: If no animator is found, just hide it so the player isn't stuck
        elevatorDoor.SetActive(false);
        elevatorBox.SetActive(false); 
        indicatorLight.material = greenMaterial;
        
    }


    void OnDestroy()
    {
        if (socket != null)
            socket.selectEntered.RemoveListener(OnKeycardInserted);
    }
}
