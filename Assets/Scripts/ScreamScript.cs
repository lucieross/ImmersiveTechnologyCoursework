using UnityEngine;
using System.Collections;

public class SequenceCounter : MonoBehaviour
{

    public int requiredCount = 3;
    private int currentCount = 0;

    public bool playOnlyWhenPowerIsOff = true;
    public AudioSource firstAudio;
    public AudioSource secondAudio;
    public LightingManager powerScript;
    public float delayBetweenSounds = 3.0f;

    private bool hasFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasFinished && powerScript.powerCut == playOnlyWhenPowerIsOff)
        {
            currentCount++;

            if (currentCount >= requiredCount)
            {
                StartCoroutine(PlayAudioSequence());
            }
        }
    }

    IEnumerator PlayAudioSequence()
    {
        hasFinished = true; // Prevents the sequence from starting twice

        // Play the first audio 
        if (firstAudio != null) 
        {
            firstAudio.Play();
        }

        // Wait for 3 seconds
        yield return new WaitForSeconds(delayBetweenSounds);

        // Play the second audio 
        if (secondAudio != null) 
        {
            secondAudio.Play();
        }
    }
}
