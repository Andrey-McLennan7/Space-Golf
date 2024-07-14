using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : MonoBehaviour
{
    [SerializeField] bool inGravityZone = false;
    
    public Rigidbody player;

    public float force = 1.0f;

    AudioSource warpSound;

    private void Start()
    {
        this.warpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check every iteration if the player is in the anti-gravity zone
        // of the cylinder
        if (this.inGravityZone)
        {
            // Add force slowly in the upwards direction
            this.player.AddForce(new Vector3(0.0f, this.force * this.player.mass, 0.0f));

            // Don't play the sound effect if the player is kinematic
            // while in the cylinder
            if (this.player.isKinematic)
            {
                this.warpSound.Stop();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player is in the anti-gravity cylinder...
        if (other.gameObject.tag == "Player")
        {
            // ... set the anti-gravity force to true
            this.inGravityZone = true;

            // Play audio
            this.warpSound.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the player is out the anti-gravity cylinder...
        if (other.gameObject.tag == "Player")
        {
            // .. set the anti-gravity force to false
            this.inGravityZone = false;

            // Play audio
            this.warpSound.Stop();
        }
    }
}