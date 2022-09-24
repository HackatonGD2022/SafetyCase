using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{

    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform rearLeftWheel;
    [SerializeField] private Transform rearRightWheel;

    [SerializeField] private bool controll = false;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    public float Speed
    {
        get;
        private set;
    }

    public bool Controll
    {
        get { return controll; }
        set { controll = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
    }

    void FixedUpdate()
    {
        // Get forward/reverse acceleration from the vertical axis
        if(controll)
        {
            currentAcceleration = acceleration * Input.GetAxis("Vertical");
            currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.Space))
                currentBreakForce = breakingForce;
            else
                currentBreakForce = 0f;

            ApplyAcceleration();
            ApplyBrake();
            ApplySterring();
            UpdateWheels();
        }
    }
    private void ApplyAcceleration()
    {
        // Apply acceleration to rear wheels
        rearLeftCollider.motorTorque = currentAcceleration;
        rearRightCollider.motorTorque = currentAcceleration;
    }

    private void ApplyBrake()
    {
        // Apply brake
        frontLeftCollider.brakeTorque = currentBreakForce;
        frontRightCollider.brakeTorque = currentBreakForce;
        rearLeftCollider.brakeTorque = currentBreakForce  * 0.9f;
        rearRightCollider.brakeTorque = currentBreakForce * 0.9f;
    }

    private void ApplySterring()
    {
        // Set steering
        frontLeftCollider.steerAngle = currentTurnAngle;
        frontRightCollider.steerAngle = currentTurnAngle;
    }

    public void UpdateWheels()
    {
        UpdateWheel(frontLeftCollider, frontLeftWheel);
        UpdateWheel(frontRightCollider, frontRightWheel);
        UpdateWheel(rearLeftCollider, rearLeftWheel);
        UpdateWheel(rearRightCollider, rearRightWheel);
    }

    private void UpdateWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        wheel.position = position;
        wheel.rotation = rotation;
    }

    public void Accelerate(float a)
    {
        currentAcceleration = a;
        ApplyAcceleration();
    }

    public void Brake(float b)
    {
        currentBreakForce = b;
        ApplyBrake();
    }

    public void Turn(float angle)
    {
        currentTurnAngle = angle;
        ApplySterring();
    }
}
