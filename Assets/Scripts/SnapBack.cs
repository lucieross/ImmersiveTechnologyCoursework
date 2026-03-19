using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapBackItem : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Call this when the "Select Exited" event triggers in the Inspector
    public void CheckReset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        
        // Reset physics so it doesn't keep moving
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}