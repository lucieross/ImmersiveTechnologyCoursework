using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TagSocketFilter : MonoBehaviour
{
    public string requiredTag = "KeyCard"; 
    private XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.hoverEntered.AddListener(CheckTag);
    }

    private void CheckTag(HoverEnterEventArgs args)
    {
        // Check if the object being hovered over the socket has the right tag
        if (!args.interactableObject.transform.CompareTag(requiredTag))
        {
            Debug.Log("Wrong item! This socket only takes: " + requiredTag);
        }
    }
}