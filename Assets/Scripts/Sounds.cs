using UnityEngine;
using System.Collections; // Required for the timer

public class Sounds : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] creepySounds;
    
    public bool playOnlyWhenPowerIsOff = true;
    public LightingManager powerScript;
    
    [Header("Timing")]
    public float soundDelay = 1.0f;
    public float cooldownMinutes = 2.0f; 

    private bool isReady = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isReady)
        {
            if (powerScript != null && powerScript.powerCut == playOnlyWhenPowerIsOff)
            {
                StartCoroutine(PlayAndReset());
            }
        }
    }

    IEnumerator PlayAndReset()
    {
        isReady = false; 

        yield return new WaitForSeconds(soundDelay);

        if (creepySounds.Length > 0)
        {
            // Roll a random number between 0 and 100
            float roll = Random.Range(0f, 100f);
            int selectedIndex = 0;

            if (roll <= 10f) 
            {
                selectedIndex = 0; // The Scream
            }
            else 
            {
                // 90% chance to pick any OTHER sound in the list
                selectedIndex = Random.Range(1, creepySounds.Length);
            }

            source.clip = creepySounds[selectedIndex];
            source.Play();
        }

        yield return new WaitForSeconds(cooldownMinutes * 60f);
        isReady = true; 
    }
}