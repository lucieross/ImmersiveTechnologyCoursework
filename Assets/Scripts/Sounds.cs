using UnityEngine;

public class PowerDependentSound : MonoBehaviour
{
    public AudioSource soundToPlay;
    public bool playOnlyWhenPowerIsOff = true;
    public LightingManager powerScript;

    public float soundDelay = 0f;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider healthcare)
    {
        if (powerScript != null && powerScript.powerCut == playOnlyWhenPowerIsOff && !hasPlayed)
        {
            soundToPlay.PlayDelayed(soundDelay);

            hasPlayed = true; 
        }
    }
}