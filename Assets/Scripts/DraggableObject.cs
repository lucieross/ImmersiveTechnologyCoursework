using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableObject : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;
    private Transform dragAnchor;

    [Header("Settings")]
    [SerializeField] private float dragSpeed = 10f;
    [SerializeField] private bool stayUpright = true;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip Thud;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.2f, 0);
        rb.sleepThreshold = 0.05f;
    }

    public void StartDragging(Transform anchor)
    {
        dragAnchor = anchor;
        isDragging = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        if (stayUpright)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void StopDragging()
    {
        isDragging = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

        if (stayUpright)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void FixedUpdate()
    {
        if (isDragging && dragAnchor != null)
        {
            Vector3 target = new Vector3(dragAnchor.position.x, rb.position.y, dragAnchor.position.z);
            Vector3 desiredVelocity = (target - rb.position).normalized * dragSpeed;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, desiredVelocity, 0.08f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 3f)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(Thud);
        }
    }
}