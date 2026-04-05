using UnityEngine;

public class DrawerSound : MonoBehaviour
{
    public AudioSource slideSound;
    private Rigidbody rb;
    private bool isBeingMoved = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        slideSound.loop = true;
        slideSound.Stop();
    }

    // Only run the logic if the drawer is actually "awake" and moving
    void FixedUpdate() 
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed > 0.02f)
        {
            if (!slideSound.isPlaying) slideSound.Play();
            slideSound.volume = Mathf.Min(speed * 0.5f, 1f);
        }
        else if (slideSound.isPlaying)
        {
            slideSound.Stop();
        }
    }
}