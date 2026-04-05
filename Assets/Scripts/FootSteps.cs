using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip[] footstepSounds;
    public float stepInterval = 0.5f;
    public float velocityThreshold = 0.1f;

    private CharacterController controller;
    private float stepTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller != null && controller.isGrounded && controller.velocity.magnitude > 0.5f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0;
        }
    }


    void PlayFootstep()
    {
        // Pick a random start time between 0 and 37 seconds (clip is 38 secs)
        float randomStartTime = Random.Range(0f, 37.0f);

        // Apply the jump
        footstepSource.time = randomStartTime;
        footstepSource.pitch = Random.Range(0.85f, 1.15f);
        footstepSource.Play();
    }
}