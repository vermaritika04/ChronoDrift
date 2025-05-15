
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private bool isBraking;
    private bool isEngineOn = false;

    [Header("Car Physics Settings")]
    [SerializeField] private float motorForce = 2500f;
    [SerializeField] private float brakeForce = 5000f;
    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private float rollingResistance = 0.985f;
    [SerializeField] private float antiRollStiffness = 5000f;

    private Rigidbody rb;
    private float currentSteerAngle = 0f;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI gearText;
    [SerializeField] private TextMeshProUGUI brakeStatusText;
    [SerializeField] private TextMeshProUGUI engineStatusText;

    // Player Input Actions
    private PlayerInputActions playerInputActions;

    [Header("Lap Management")]
    [SerializeField] private GameObject lapTrigger;
    [SerializeField] private List<GameObject> checkpoints;
    private int currentCheckpointIndex = 0;

    private int currentGear = 0;
    private int currentLap = 1;
    private int totalLaps = 3;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found!");
            return;
        }

        rb.mass = 1200f;
        rb.drag = 0.1f;
        rb.angularDrag = 2f;
        rb.centerOfMass = new Vector3(0, -0.9f, 0);
        rb.constraints = RigidbodyConstraints.None;

        AdjustWheelFriction();
        AdjustWheelSuspension();
        UpdateEngineStatusUI();

        // Input actions binding
        playerInputActions.Player.Move.performed += ctx => OnMove(ctx);
        playerInputActions.Player.Move.canceled += ctx => OnMoveCancel(ctx);
        playerInputActions.Player.HandBrake.performed += ctx => OnHandBrake();
        playerInputActions.Player.ToggleEngine.performed += ctx => ToggleEngineImmediate();
    }

    private void Update()
    {
        if (!isEngineOn) return;

        UpdateGear();
        HandleMotor();
        HandleSteering();
        ApplyBraking();
        ApplyRollingResistance();
        ApplyStabilizer();
        UpdateWheels();
        UpdateUI();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        horizontalInput = 0;
        verticalInput = 0;
    }

    private void OnHandBrake()
    {
        isBraking = true;
    }

    private void ToggleEngineImmediate()
    {
        isEngineOn = !isEngineOn;
        UpdateEngineStatusUI();
        Debug.Log("Engine toggled: " + (isEngineOn ? "ON" : "OFF"));
    }

    private void FixedUpdate()
    {
        if (!isEngineOn) return;
        // Physics updates
    }

    private void UpdateGear()
    {
        float velocityZ = Vector3.Dot(rb.velocity, transform.forward);

        if (verticalInput > 0)
            currentGear = 1;
        else if (verticalInput < 0 && velocityZ < -0.1f)
            currentGear = -1;
        else
            currentGear = 0;
    }

    private void HandleMotor()
    {
        float appliedMotorForce = verticalInput * motorForce;

        frontLeftWheelCollider.motorTorque = appliedMotorForce * 0.35f;
        frontRightWheelCollider.motorTorque = appliedMotorForce * 0.35f;
        rearLeftWheelCollider.motorTorque = appliedMotorForce * 0.65f;
        rearRightWheelCollider.motorTorque = appliedMotorForce * 0.65f;
    }

    private void ApplyBraking()
    {
        float appliedBrakeForce = isBraking ? brakeForce : 0f;

        frontRightWheelCollider.brakeTorque = appliedBrakeForce;
        frontLeftWheelCollider.brakeTorque = appliedBrakeForce;
        rearLeftWheelCollider.brakeTorque = appliedBrakeForce;
        rearRightWheelCollider.brakeTorque = appliedBrakeForce;

        isBraking = false;
    }

    private void ApplyRollingResistance()
    {
        if (Mathf.Abs(verticalInput) < 0.1f && !isBraking)
            rb.velocity *= rollingResistance;
    }

    private void HandleSteering()
    {
        float targetSteerAngle = maxSteerAngle * horizontalInput * 0.7f;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, Time.deltaTime * 6f);

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void ApplyStabilizer()
    {
        ApplyAntiRoll(frontLeftWheelCollider, frontRightWheelCollider);
        ApplyAntiRoll(rearLeftWheelCollider, rearRightWheelCollider);
    }

    private void ApplyAntiRoll(WheelCollider leftWheel, WheelCollider rightWheel)
    {
        WheelHit hit;
        float travelLeft = 1f;
        float travelRight = 1f;

        bool groundedLeft = leftWheel.GetGroundHit(out hit);
        if (groundedLeft)
            travelLeft = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;

        bool groundedRight = rightWheel.GetGroundHit(out hit);
        if (groundedRight)
            travelRight = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;

        float antiRollForce = (travelLeft - travelRight) * antiRollStiffness;

        if (groundedLeft)
            rb.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
        if (groundedRight)
            rb.AddForceAtPosition(rightWheel.transform.up * antiRollForce, rightWheel.transform.position);
    }

    private void AdjustWheelFriction()
    {
        WheelCollider[] wheels = {
            frontLeftWheelCollider, frontRightWheelCollider,
            rearLeftWheelCollider, rearRightWheelCollider
        };

        foreach (WheelCollider wheel in wheels)
        {
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.stiffness = 1.5f;
            wheel.forwardFriction = forwardFriction;

            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.stiffness = 2.0f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }

    private void AdjustWheelSuspension()
    {
        WheelCollider[] wheels = {
            frontLeftWheelCollider, frontRightWheelCollider,
            rearLeftWheelCollider, rearRightWheelCollider
        };

        foreach (WheelCollider wheel in wheels)
        {
            JointSpring spring = wheel.suspensionSpring;
            spring.spring = 35000f;
            spring.damper = 4500f;
            wheel.suspensionSpring = spring;

            wheel.suspensionDistance = 0.2f;
        }
    }

    private void UpdateEngineStatusUI()
    {
        if (engineStatusText != null)
            engineStatusText.text = isEngineOn ? "Engine: ON" : "Engine: OFF";
    }

    private void UpdateUI()
    {
        float speed = GetSpeed();
        if (speedText != null)
            speedText.text = "Speed: " + Mathf.Round(speed) + " km/h";

        if (gearText != null)
        {
            if (currentGear == 1)
                gearText.text = "F";
            else if (currentGear == -1)
                gearText.text = "R";
            else
                gearText.text = "N";
        }

        if (brakeStatusText != null)
            brakeStatusText.text = isBraking ? "Braking: ON" : "Braking: OFF";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == checkpoints[currentCheckpointIndex])
        {
            currentCheckpointIndex++;

            // If all checkpoints passed, wait for lap trigger
            if (currentCheckpointIndex >= checkpoints.Count)
            {
                currentCheckpointIndex = 0; // Reset for the next lap
            }
        }

        if (other.gameObject == lapTrigger && currentCheckpointIndex == 0)
        {
            if (currentLap < totalLaps)
            {
                currentLap++;
                Debug.Log("Lap Completed! Current Lap: " + currentLap);
            }
            else
            {
                Debug.Log("Race Finished!");
                // You can implement race finish logic here
            }
        }
    }


    public float GetSpeed() => rb.velocity.magnitude * 3.6f;
    public int GetCurrentGear() => currentGear;
    public bool IsBraking() => isBraking;
    public int GetCurrentLap() => currentLap;
    public int GetTotalLaps() => totalLaps;
}





