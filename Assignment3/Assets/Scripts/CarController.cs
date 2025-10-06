using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    // colliders for all wheels
    public WheelCollider fLWheelColl;
    public WheelCollider fRWheelColl;
    public WheelCollider rLWheelColl;
    public WheelCollider rRWheelColl;

    // meshes for all wheels
    public MeshRenderer fLWheelMesh;
    public MeshRenderer fRWheelMesh;
    public MeshRenderer rLWheelMesh;
    public MeshRenderer rRWheelMesh;

    // gets key input from the player
    public float forwardInput;
    public float steeringInput;
    public float brakeInput;

    // moves and steers the car
    public float force;
    public float speed;
    public float brake;
    public float slipAngle;
    public AnimationCurve steering;

    public bool isPlaying;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isPlaying = false;
    }

    void FixedUpdate()
    {
        // how big the vector the velocity vector is
        speed = rb.linearVelocity.magnitude;
        CheckInput();
        UpdateAllWheels(); 
        Move();
        Steer();
        Brake();

        if(forwardInput != 0 && !isPlaying)
        {
            FindFirstObjectByType<AudioManager>().Play("carSound");
            isPlaying = true;
        }
        
        if(forwardInput == 0)
        {
            FindFirstObjectByType<AudioManager>().Stop("carSound");
            isPlaying = false;
        }
    }

    void CheckInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        // gets the forward vector of the car
        slipAngle = Vector3.Angle(transform.forward, rb.linearVelocity);
        // if car is going forward...
        if(slipAngle < 120f && forwardInput < 0)
        {
            // braking power cannot be less than 0
            brakeInput = Mathf.Abs(forwardInput);
            // reset
            forwardInput = 0;
            //FindFirstObjectByType<AudioManager>().Stop("carSound");
        }
        else
        {
            // reset
            brakeInput = 0;
        }
    }

    void Brake()
    {
        // put more force on front because the car will naturally want to nosedive
        fLWheelColl.brakeTorque = brakeInput * brake * 0.7f;
        fRWheelColl.brakeTorque = brakeInput * brake * 0.7f;
        rLWheelColl.brakeTorque = brakeInput * brake * 0.3f;
        rRWheelColl.brakeTorque = brakeInput * brake * 0.3f;
    }

    void Steer()
    {
        // evaluates how fast you're going to properly apply the angle you're trying to turn at
        float steeringAngle = steeringInput * steering.Evaluate(speed);

        // apply the steering angle to turn
        fLWheelColl.steerAngle = steeringAngle;
        fRWheelColl.steerAngle = steeringAngle;

    }

    void Move()
    {
        rLWheelColl.motorTorque = force * forwardInput;
        rRWheelColl.motorTorque = force * forwardInput;   
    }

    void UpdateAllWheels()
    {
        UpdateWheel(fLWheelColl, fLWheelMesh);
        UpdateWheel(fRWheelColl, fRWheelMesh);
        UpdateWheel(rLWheelColl, rLWheelMesh);
        UpdateWheel(rRWheelColl, rRWheelMesh);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer mesh)
    {
        // moves the wheels and updates their meshes to match
        Quaternion quat;
        Vector3 pos;

        coll.GetWorldPose(out pos, out quat);
        mesh.transform.position = pos;
        mesh.transform.rotation = quat;
    }
}
