using UnityEngine;

public class PlayLockedSound : MonoBehaviour
{
    [SerializeField] Lock lockScript; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lockScript.locked)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
