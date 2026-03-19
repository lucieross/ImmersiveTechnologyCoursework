using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeedback : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color originalColor;
    public Color highlightColor = Color.green; 

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;

        // Automatically find the XR Grab component on this object
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        
        interactable.hoverEntered.AddListener(OnHover);
        interactable.hoverExited.AddListener(OnExit);
    }

    void OnHover(HoverEnterEventArgs args) => meshRenderer.material.color = highlightColor;
    void OnExit(HoverExitEventArgs args) => meshRenderer.material.color = originalColor;
}