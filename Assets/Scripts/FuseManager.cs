using UnityEngine;
using UnityEngine.Events;

public class FuseManager : MonoBehaviour
{
    private int fusesInstalled = 0;
    public int requiredFuses = 3;
    public GameObject rewireMiniGame;
 

    public UnityEvent onAllFusesInstalled;

    public void FuseAdded()
    {
        fusesInstalled++;
        if (fusesInstalled >= requiredFuses)
        {
            rewireMiniGame.SetActive(true);
            CompleteCircuit();
        }

    }

    public void FuseRemoved()
    {
        fusesInstalled--;
    }



    void CompleteCircuit()
    {
        rewireMiniGame.SetActive(true);
        onAllFusesInstalled.Invoke();

    }

}