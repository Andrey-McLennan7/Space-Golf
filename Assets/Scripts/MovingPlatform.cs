using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float lerpTime;

    bool moveUp;
    float t;

    void Start()
    {
        this.t = Time.deltaTime;
        moveUp = true;
    }

    void Update()
    {
        if (this.t < this.lerpTime && this.moveUp)
        {
            this.transform.position = Vector3.Lerp(pointA.position, pointB.position, t / lerpTime);
            this.t += Time.deltaTime;

            if (this.t >= this.lerpTime)
            {
                this.moveUp = false;
            }
        }
        else
        {
            this.transform.position = Vector3.Lerp(pointA.position, pointB.position, t / lerpTime);
            this.t -= Time.deltaTime;

            if (this.t <= 0)
            {
                this.moveUp = true;
            }
        }
    }
}