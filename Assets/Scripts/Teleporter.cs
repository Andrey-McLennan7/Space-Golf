using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform other_position;

    [SerializeField] Transform player;

    AudioSource teleportSound;

    void Start()
    {
        this.teleportSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // First, check which object activated the object's
        // trigger on collision
        if (other.gameObject.tag == "Player")
        {
            // If it's the player, then teleport the player near the other
            // teleporter (to prevent an infinite teleporting loop)
            this.player = other.transform;
            StartCoroutine("Teleport");

            // Play sound effect
            this.teleportSound.Play();
        }
    }

    IEnumerator Teleport()
    {
        this.player.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(0.01f);

        this.player.position = this.other_position.transform.position;

        yield return new WaitForSeconds(0.01f);

        this.player.GetComponent<Rigidbody>().isKinematic = false;
    }
}