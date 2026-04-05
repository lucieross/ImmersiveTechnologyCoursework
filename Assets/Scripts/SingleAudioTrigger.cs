using UnityEngine;

public class PowerDependentSound : MonoBehaviour
{
    public AudioSource soundToPlay;
    public bool playOnlyWhenPowerIsOff = true;
    public LightingManager powerScript;
    public float soundDelay = 0f;
    public float audioStartAt = 0f;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider healthcare)
    {
        if (powerScript != null && powerScript.powerCut == playOnlyWhenPowerIsOff && !hasPlayed)
        {
            soundToPlay.time = audioStartAt;
            soundToPlay.PlayDelayed(soundDelay);

            hasPlayed = true;
        }
    }
}