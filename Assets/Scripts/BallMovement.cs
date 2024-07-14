using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    bool isIdle;
    float xRot = 0.0f;
    float yRot = 0.0f;
    float distToGround = 0.0f;

    public float rotationSpeed = 100.0f;

    public float shotPower = 20.0f;
    public float maxPower = 50.0f;
    public float minPower = 10.0f;

    public float powerSpeed = 8.0f;

    public float stopVelocity = 0.5f;

    GameObject indicator;
    GameObject point;

    Rigidbody this_rb;

    AudioSource shotSound;

    void Start()
    {
        this.this_rb = GetComponent<Rigidbody>();
        this.isIdle = true;
        this.this_rb.isKinematic = true;

        this.indicator = GameObject.Find("Indicator");
        this.point = GameObject.Find("ArrowPoint");

        this.distToGround = GetComponent<Collider>().bounds.extents.y;

        this.shotSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // First, check if the ball is idle
        if (this.isIdle)
        {
            // if true, then...
            this.RotateBall();
            this.Power();
            this.LaunchBall();
        }
        else
        {
            // if false (not moving), then wait until the ball is grounded
            // and stops moving, which will set the idle back to true
            if (this.this_rb.velocity.magnitude < this.stopVelocity)
            {
                if (this.isGrounded())
                {
                    this.Stop();
                }
            }
        }
    }

    private void RotateBall()
    {
        // ...you can control the ball's rotation

        // Press 'W' to move up; press 'S' to move down
        this.xRot -= (Input.GetAxis("Vertical") * this.rotationSpeed) * Time.deltaTime;
        // Press 'A' to move left; press 'D' to move right
        this.yRot += (Input.GetAxis("Horizontal") * this.rotationSpeed) * Time.deltaTime;

        // Restrict the user's vertical movement between -45 and 0
        this.xRot = Mathf.Clamp(this.xRot, -45.0f, 0.0f);

        this.transform.rotation = Quaternion.Euler(this.xRot, this.yRot, 0.0f);
    }

    private void Power()
    {
        // ...you can control the launch power
        if (Input.GetKey(KeyCode.E))
        {
            if (this.shotPower < this.maxPower)
            {
                // Increase power
                this.shotPower += this.powerSpeed * Time.deltaTime;
                this.indicator.gameObject.transform.localScale += new Vector3(0, 0, 1 * Time.deltaTime);
                this.indicator.gameObject.transform.Translate(0, 0, 0.5f * Time.deltaTime);
            }
            else
            {
                // If the increase in shot power excedes the maximum
                // amount of power allowed by the game, then set the
                // power to that maximum value
                this.shotPower = this.maxPower;
            }
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            if (this.shotPower > this.minPower)
            {
                // Decrease power
                this.shotPower -= this.powerSpeed * Time.deltaTime;
                this.indicator.gameObject.transform.localScale -= new Vector3(0, 0, 1 * Time.deltaTime);
                this.indicator.gameObject.transform.Translate(0, 0, -0.5f * Time.deltaTime);
            }
            else
            {
                // If the increase in shot power is lower than the minimum
                // amount of power allowed by the game, then set the power
                // to that minimum value
                this.shotPower = this.minPower;
            }
        }
    }

    private void LaunchBall()
    {
        // ...you can press SPACE to launch the ball
        // in the direction the indicator is pointing at
        if (Input.GetKey(KeyCode.Space))
        {
            // Unidle the ball when launched
            this.isIdle = false; // controls
            this.this_rb.isKinematic = false; // movement
            this.indicator.GetComponent<MeshRenderer>().forceRenderingOff = true; // remove indicator
            this.point.GetComponent<MeshRenderer>().forceRenderingOff = true; // remove arrow point

            // Play sound effect
            this.shotSound.Play();

            // Ball launcher
            this.this_rb.AddForce(this.transform.forward * this.shotPower * this.this_rb.mass, ForceMode.Impulse);
        }
    }

    private void Stop()
    {
        // Set the ball back to idle
        this.isIdle = true; // controls
        this.this_rb.isKinematic = true; // movement
        this.indicator.GetComponent<MeshRenderer>().forceRenderingOff = false; // return indicator
        this.point.GetComponent<MeshRenderer>().forceRenderingOff = false; // return arrow point
    }

    private bool isGrounded()
    {
        // Use raycasting to detect if the ball is on the ground
        return Physics.Raycast(this.transform.position, Vector3.down, this.distToGround + 0.1f);
    }
}