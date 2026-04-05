using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FuseManager : MonoBehaviour
{
    private int fusesInstalled = 0;
    public int requiredFuses = 3;
    public GameObject keyCard;

    public AudioSource ElectricalSound;
    public AudioSource Insert;
    public AudioSource NotEnoughFusesVoiceLine;
    public AudioSource CompletedFuses;

    public UnityEvent onAllFusesInstalled;

    public void FuseAdded()
    {
        Insert.Play();
        fusesInstalled++;

        if (fusesInstalled >= requiredFuses)
        {
            CompleteCircuit();
        }
    }

    public void CheckProgress()
    {
        if (fusesInstalled < requiredFuses)
        {
            if (!NotEnoughFusesVoiceLine.isPlaying)
            {
                NotEnoughFusesVoiceLine.Play();
            }
        }
    }

    public void FuseRemoved()
    {
        fusesInstalled--;
    }

    void CompleteCircuit()
    {
        CompletedFuses.Play();
        keyCard.SetActive(true);
        onAllFusesInstalled.Invoke();
        ElectricalSound.Play();
    }

    public void TryInteract(SelectEnterEventArgs args)
    {
        IXRSelectInteractor hand = args.interactorObject;
        if (hand.firstInteractableSelected == null)
        {
            CheckProgress();
        }
    }
}