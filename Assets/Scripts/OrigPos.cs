using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrigPos : MonoBehaviour
{
    [SerializeField] Vector3 orig_pos;

    [SerializeField] bool inLevel;

    AudioSource respawn;

    void Start()
    {
        this.orig_pos = this.transform.position;

        this.respawn = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!this.inLevel)
        {
            StartCoroutine("Respawn");

            // Play audio
            this.respawn.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            this.inLevel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            this.inLevel = false;
        }
    }

    IEnumerator Respawn()
    {
        this.transform.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(0.01f);

        this.transform.position = this.orig_pos;

        yield return new WaitForSeconds(0.01f);

        this.transform.GetComponent<Rigidbody>().isKinematic = false;
    }
}