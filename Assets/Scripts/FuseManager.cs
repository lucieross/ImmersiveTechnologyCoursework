using UnityEngine;
using UnityEngine.Events;

public class FuseManager : MonoBehaviour
{
    private int fusesInstalled = 0;
    public int requiredFuses = 3;

    // You can hook up your "Power On" function here in the Inspector 

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
        onAllFusesInstalled.Invoke();

    }

}