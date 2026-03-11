using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public partial class HandAnimator : MonoBehaviour
{
    [SerializeField] private NearFarInteractor nearFarInteractor;
    [SerializeField] private SkinnedMeshRenderer handMesh;
    [SerializeField] private GameObject handArmature; 
    [SerializeField] private InputActionReference selectActionRef;
    [SerializeField] private InputActionReference activateActionRef;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private float actionDelay = 0.3f;

    private static readonly int ActivateAnim = Animator.StringToHash("activate");
    private static readonly int SelectAnim = Animator.StringToHash("select");
    
    private bool _isHandEmpty = true; // Initialize to true so animations work at start

    private void OnEnable()
    {
        // Make sure the actions are active so we can read their values
        if (selectActionRef != null) selectActionRef.action.Enable();
        if (activateActionRef != null) activateActionRef.action.Enable();
    }

    private void OnDisable()
    {
        // Clean up when the hand is hidden/destroyed
        if (selectActionRef != null) selectActionRef.action.Disable();
        if (activateActionRef != null) activateActionRef.action.Disable();
    }
    private void Awake()
    {
        nearFarInteractor.selectEntered.AddListener(OnGrab);
        nearFarInteractor.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        _isHandEmpty = false;
        StartCoroutine(DelayedHandState(false)); 
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        _isHandEmpty = true;
        StartCoroutine(DelayedHandState(true));
    }

    private IEnumerator DelayedHandState(bool showHand)
    {
        yield return new WaitForSeconds(actionDelay); 
        
        handMesh.enabled = showHand;
        handArmature.SetActive(showHand);
    }

    void Update()
    {
        if (_isHandEmpty)
        {
            handAnimator.SetFloat(ActivateAnim, activateActionRef.action.ReadValue<float>());
            handAnimator.SetFloat(SelectAnim, selectActionRef.action.ReadValue<float>());
        }
    }
}