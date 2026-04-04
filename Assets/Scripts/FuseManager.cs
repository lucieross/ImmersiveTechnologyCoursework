using UnityEngine;
using UnityEngine.Events;

public class FuseManager : MonoBehaviour
{
    private int fusesInstalled = 0;
    public int requiredFuses = 3;
    public GameObject keyCard;
 

    public UnityEvent onAllFusesInstalled;

    public void FuseAdded()
    {
        fusesInstalled++;
        if (fusesInstalled >= requiredFuses)
        {
            CompleteCircuit();
        }

    }

    public void FuseRemoved()
    {
        fusesInstalled--;
    }



    void CompleteCircuit()
    {
        keyCard.SetActive(true);
        onAllFusesInstalled.Invoke();

    }

}