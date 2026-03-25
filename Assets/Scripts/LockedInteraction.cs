using UnityEngine;
using TMPro; // Make sure you have TextMeshPro installed

public class VRLockedTrigger : MonoBehaviour
{
    public GameObject lockedUI; // Drag your World Space Canvas here
    public float displayTime = 2.0f;

    void Start()
    {
        if (lockedUI != null) lockedUI.SetActive(false);
    }

    // This detects when your VR hand (with a collider) enters the cupboard area
    private void OnTriggerEnter(Collider healthcare)
    {
        // Check if the thing touching the cupboard is the player's hand/controller
        if (healthcare.gameObject.CompareTag("Player") || healthcare.gameObject.name.Contains("Hand") || healthcare.gameObject.name.Contains("Controller"))
        {
            ShowLocked();
        }
    }

    public void ShowLocked()
    {
        CancelInvoke("HideLocked");
        lockedUI.SetActive(true);
        Invoke("HideLocked", displayTime);
    }

    void HideLocked()
    {
        lockedUI.SetActive(false);
    }
}