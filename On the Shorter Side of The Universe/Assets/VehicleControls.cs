using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class VehicleControls : MonoBehaviour
{
    // Start is called before the first frame update
    private bool vehicleStarted = false;
    private float velocity = 0, vertical,horizontal;

    private int tick = 0;

    [Header("Vehicle Properties")]
    [SerializeField] private float maxSpeed = 15;

    [Header("Dependencies")]
    [SerializeField] private GameObject EventSystem;
    [SerializeField] private TMP_Text DisplayText;

    private Vector3 lastPos;
    void Start()
    {
        EventSystem.GetComponent<EventScripts>().playerIsInVehicleEvent += StartVehicle;
    }

    void StartVehicle()
    {
        vehicleStarted = true;
        DisplayText.text = "Speed " + velocity;
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

        float currentSpeed= (currentPos - lastPos).magnitude;
        lastPos = currentPos;
        return currentSpeed;
        
    }
     
    // Update is called once per frame
    void FixedUpdate()
    {
        if (vehicleStarted)
        {
            ScanInput();
            //this will be used for top speed limiting
            velocity = calculateSpeed(transform.position);
            DisplayText.text = "Speed " + velocity;
            if (velocity < maxSpeed)
            {
                if (StaticVariables.DebugVehicleDynamics)
                {
                    Debug.Log("Horizontal" + horizontal);
                    Debug.Log("Vertical" + vertical);
                    Debug.Log(velocity);
                }
                GetComponent<Rigidbody>().AddForce(new Vector3(0,0,1)*-1000);
            }
        }
    }
}
