using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EssentialPickupSound : MonoBehaviour
{
    public AudioSource soundSource; 
    public AudioClip pickupClip;
    
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(PlaySound);
        }
    }
    private void PlaySound(SelectEnterEventArgs args)
    {
        if (soundSource != null && pickupClip != null)
        {
            soundSource.PlayOneShot(pickupClip);
            this.enabled = false; 
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(PlaySound);
        }
    }
}