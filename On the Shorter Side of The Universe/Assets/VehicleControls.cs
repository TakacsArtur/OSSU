using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class VehicleControls : MonoBehaviour
{
    // Start is called before the first frame update
    private bool vehicleStarted = false;
    private float velocity = 0, vertical, horizontal;

    [Header("Vehicle Properties")]
    [SerializeField] private float maxSpeed = 15;

    [Header("Dependencies")]
    [SerializeField] private GameObject EventSystem;
    [SerializeField] private TMP_Text LeftDisplayText, RightBpttomText, RightTopTextt;
    [SerializeField] private float dragDefault;

    private enum VehicleGear { Neutral, Drive, Reverse };

    private enum VehicleState { Stationary, SlowForward, FastForward, BackWards, Crashed }

    private enum VehicleDriveMode { Comfort, Sport };

    private VehicleDriveMode currentDriveMode = VehicleDriveMode.Comfort;
    private VehicleGear currentGear = VehicleGear.Neutral;
    private VehicleState currentState = VehicleState.Stationary;


    private Vector3 lastPos;
    void Start()
    {
        EventSystem.GetComponent<EventScripts>().playerIsInVehicleEvent += StartVehicle;
    }

    void StartVehicle()
    {
        vehicleStarted = true;
        LeftDisplayText.text = "Speed " + velocity;
    }

    void ScanInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        horizontal *= 1F;
        vertical *= 1F;
    }

    float calculateSpeed(Vector3 currentPos)
    {

        float currentSpeed = (currentPos - lastPos).magnitude;
        lastPos = currentPos;
        return currentSpeed;

    }

    VehicleState DetermineVehicleState()
    {
        if (Math.Round(calculateSpeed(transform.position), 3) == 0)
        {
            return VehicleState.Stationary;
        }


        else return VehicleState.Stationary;
    }

    void DriveUserDisplay()
    {
        LeftDisplayText.text = "Speed " + Math.Round(velocity, 1) * 25;
    }
    


    void AddDrivingForce(VehicleState currentState)
    {
        if (currentState == VehicleState.FastForward || currentState == VehicleState.SlowForward)
        {
            //we are accelerating
            if (vertical > 0)
            {
                GetComponent<Rigidbody>().AddForce(transform.right * (vertical * 30), ForceMode.Acceleration);
                GetComponent<Rigidbody>().drag = dragDefault;
            }
            if (vertical < 0)
            {
                GetComponent<Rigidbody>().drag += 0.2F;
            }
        }
        

    }

    void AddCorneringSpeed(VehicleState currentState)
    {

        if (currentState == VehicleState.FastForward || currentState == VehicleState.SlowForward)
        {
            transform.RotateAround(transform.position, new Vector3(0, Input.GetAxis("Horizontal"), 0), Time.deltaTime * Math.Clamp(50 - velocity * 25, 25, 50));
        }

        
        if (currentState == VehicleState.BackWards)
        {
            transform.RotateAround(transform.position, new Vector3(0, Input.GetAxis("Horizontal"), 0), Time.deltaTime *  -1 * Math.Clamp(50 - velocity * 25, 25, 50));
        }
    }

     
    // Update is called once per frame
    void FixedUpdate()
    {
        if (vehicleStarted)
        {
            ScanInput();
            //this will be used for top speed limiting
            velocity = calculateSpeed(transform.position);
            
            if (velocity < maxSpeed)
            {
                if (StaticVariables.DebugVehicleDynamics)
                {
                    Debug.Log("Horizontal" + horizontal);
                    Debug.Log("Vertical" + vertical);
                    Debug.Log(velocity);
                }

                AddDrivingForce(VehicleState.FastForward);
                AddCorneringSpeed(VehicleState.FastForward);
                
            }
        }
    }
}
