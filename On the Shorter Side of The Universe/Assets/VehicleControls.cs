using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControls : MonoBehaviour
{
    // Start is called before the first frame update
    private bool vehicleStarted = false;
    private float velocity = 0;

    [Header("Dependencies")]
    [SerializeField] private GameObject EventSystem;

    private Vector3 lastPos, currentPos;
    void Start()
    {
        
    }

    void StartVehicle()
    {
        vehicleStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (vehicleStarted)
        {
            
        }
    }
}
