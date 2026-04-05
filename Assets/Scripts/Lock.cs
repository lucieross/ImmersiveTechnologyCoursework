using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorHandle;
    [SerializeField] GameObject doorHandle2;
    [SerializeField] GameObject key;
    [SerializeField] AudioSource UnlockSound;
    public bool locked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locked = true;
        door.GetComponent<Rigidbody>().isKinematic = true;
        doorHandle.GetComponent<BoxCollider>().enabled = false;
        doorHandle2.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "key" && locked)
        {
            if (other.gameObject.name == key.gameObject.name)
            {
                unlockDoor();
            }
        }
    }

    private void unlockDoor()
    {
        door.GetComponent<Rigidbody>().isKinematic = false;
        doorHandle.GetComponent<BoxCollider>().enabled = true;
        doorHandle2.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<BoxCollider>().isTrigger = false;
        UnlockSound.Play();
        locked = false;
    }

}