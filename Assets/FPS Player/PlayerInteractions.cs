using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("InteractableInfo")]
    public float sphereCastRadius = 0.5f;
    public int interactableLayerIndex;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private FPSGrab physicsObject;
    [SerializeField] Camera mainCamera;

    [Header("Pickup")]
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDist = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;

    //A simple visualization of the point we're following in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pickupParent.position, 0.5f);
    }

    //Interactable Object detections and distance check
    void Update()
    {
        //Here we check if we're currently looking at an interactable object
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Debug.DrawRay(raycastPos, mainCamera.transform.forward, Color.green);
        if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, maxDistance, 1 << interactableLayerIndex))
        {

            lookObject = hit.collider.gameObject;

        }
        else
        {
            lookObject = null;

        }



        /*if we press the button of choice
        if (Input.GetButtonDown("Fire2"))
        {

        }
        */


    }

    public void OnGrabPressed()
    {
        //if we're not holding anything
        if (currentlyPickedUpObject == null)
        {
            //and we are looking an interactable object
            if (lookObject != null)
            {

                PickUpObject();
            }

        }
        //if we press the pickup button and have something, we drop it
        else
        {
            BreakConnection();
        }
    }


    //Velocity movement toward pickup parent and rotation
    private void FixedUpdate()
{
    if (currentlyPickedUpObject != null)
    {
        // Check if we are dragging a heavy object
        DraggableObject drag = currentlyPickedUpObject.GetComponent<DraggableObject>();
        
        // only run the lift physics if it is NOT a draggable heavy object
        if (drag == null) 
        {
           
            if (pickupRB == null)
            {
                pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
                if (pickupRB == null) pickupRB = currentlyPickedUpObject.GetComponentInChildren<Rigidbody>();
            }

            if (pickupRB != null)
            {
                currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
                currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
                currentSpeed *= Time.fixedDeltaTime;
                Vector3 direction = pickupParent.position - pickupRB.position;
                pickupRB.linearVelocity = direction.normalized * currentSpeed;
                
                lookRot = Quaternion.LookRotation(mainCamera.transform.position - pickupRB.position);
                lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
                pickupRB.MoveRotation(lookRot);
            }
        }
    }
}

    //Release the object
    public void BreakConnection()
    {
        if (currentlyPickedUpObject == null) return; // Nothing to break

        // Stop dragging if it's heavy
        DraggableObject draggable = currentlyPickedUpObject.GetComponent<DraggableObject>();
        if (draggable != null) draggable.StopDragging();

        // Stop FPSGrab if it exists
        if (physicsObject != null) physicsObject.pickedUp = false;

        // Reset constraints only if the RB exists
        if (pickupRB != null)
        {
            pickupRB.constraints = RigidbodyConstraints.None;
            pickupRB = null; // Clear this so FixedUpdate doesn't try to use it
        }

        currentlyPickedUpObject = null;
        currentDist = 0;
    }

    public void PickUpObject()
    {
        // Check if it's "Heavy" before doing anything else
        if (lookObject.CompareTag("Heavy"))
        {
            DraggableObject drag = lookObject.GetComponent<DraggableObject>();
            if (drag != null)
            {
                drag.StartDragging(pickupParent);
                currentlyPickedUpObject = lookObject;
                return; // Exit here so we don't pick it up!
            }
        }

        pickupRB = lookObject.GetComponent<Rigidbody>();
        if (pickupRB == null)
        {
            pickupRB = lookObject.GetComponentInChildren<Rigidbody>();
        }

        if (pickupRB != null)
        {
            physicsObject = lookObject.GetComponentInChildren<FPSGrab>();
            currentlyPickedUpObject = lookObject;

            pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
            physicsObject.playerInteractions = this;
            StartCoroutine(physicsObject.PickUp());
        }
        else
        {
            Debug.LogWarning("You tried to pick up " + lookObject.name + ", but it has no Rigidbody!");
        }
    }


}