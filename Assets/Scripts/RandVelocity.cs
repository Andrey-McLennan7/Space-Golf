using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandVelocity : MonoBehaviour
{
    [SerializeField] float shotPower = 0.0f;
    [SerializeField] float minPower = 0.0f;
    [SerializeField] float maxPower = 0.0f;
    [SerializeField] int randomVeocity = 0;
    [SerializeField] Vector3 direction = new Vector3(0, 0, 0);

    AudioSource randomVelocityShot;

    void Start()
    {
        this.minPower = GameObject.Find("Ball").GetComponent<BallMovement>().minPower;
        this.maxPower = GameObject.Find("Ball").GetComponent<BallMovement>().maxPower;

        this.randomVelocityShot = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // First, check which object activated the object's
        // trigger on collision
        if (other.gameObject.tag == "Player")
        {
            // If it's the player, then shoot the player into a random
            // direction with the same magnitude as the player's
            // current shot power
            this.SelectDirection();

            this.shotPower = Random.Range(this.minPower, this.maxPower + 0.1f);

            // Play sound effect
            this.randomVelocityShot.Play();

            other.GetComponent<Rigidbody>().AddForce(this.direction * this.shotPower, ForceMode.Impulse);
        }
    }

    private void SelectDirection()
    {
        // Random number generator [1, 5)
        this.randomVeocity = Random.Range(1, 5);

        switch (this.randomVeocity)
        {
            case 1:
                // Shoot forwards from the object's forward vector
                this.direction = this.transform.forward;
                Debug.Log("Random velocity: " + this.randomVeocity + "; Direction: forwards");
                break;
            case 2:
                // Shoot backwards from the object's forward vector reversed
                this.direction = -this.transform.forward;
                Debug.Log("Random velocity: " + this.randomVeocity + "; Direction: backwards");
                break;
            case 3:
                // Shoot right from the object's right vector
                this.direction = this.transform.right;
                Debug.Log("Random velocity: " + this.randomVeocity + "; Direction: right");
                break;
            case 4:
                // Shot left from the object's forward vector reversed
                this.direction = -this.transform.right;
                Debug.Log("Random velocity: " + this.randomVeocity + "; Direction: left");
                break;
        }
    }
}