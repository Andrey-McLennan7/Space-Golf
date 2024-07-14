using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float xRot, yRot = 0.0f;
    Rigidbody ball;

    public float rotationSpeed = 5.0f;

    void Start()
    {
        // Make sure that the camera follows the player by default
        this.ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        this.transform.position = this.ball.position;

        // Always receive the mouse pointer's position
        this.xRot += Input.GetAxis("Mouse X") * this.rotationSpeed;
        this.yRot += Input.GetAxis("Mouse Y") * this.rotationSpeed;

        // Make sure that the camera does not vertically go beyond
        // the position limitation
        this.yRot = Mathf.Clamp(this.yRot, -15.0f, 30.0f);

        this.transform.rotation = Quaternion.Euler(this.yRot, this.xRot, 0.0f);
    }
}