using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform player;
    public Transform spawnPoint;
    
    bool inLevel;

    AudioSource respawnSound;

    void Start()
    {
        this.respawnSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // If the player is outside the level (i.e., outside the box)
        if (!this.inLevel)
        {
            // Respawn them back at the beginning
            StartCoroutine("Respawn");

            // Play audio
            this.respawnSound.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // The player is in the level
        if (other.gameObject.tag == "Player")
        {
            this.inLevel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // The player is outside the level
        if (other.gameObject.tag == "Player")
        {
            this.inLevel = false;
        }
    }

    IEnumerator Respawn()
    {
        this.player.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(0.01f);

        this.player.position = this.spawnPoint.position;

        yield return new WaitForSeconds(0.01f);

        this.player.GetComponent<Rigidbody>().isKinematic = false;
    }
}