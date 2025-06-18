using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleEnter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player, EventSystem;
    [SerializeField] private TMP_Text actionInformation;

    void Start()
    {
        EventSystem.GetComponent<EventScripts>().playerRequestedActionEvent += EnterVehicle;
    }

    //the player is close to the vehicle
    private bool entryPermitted;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(player.gameObject.name))
        {
            actionInformation.text = "Enter vehicle";
            entryPermitted = true;
            if (StaticVariables.DebugEntry)
                Debug.Log("Vehicle Entry permitted");
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals(player.gameObject.name))
        {
             actionInformation.text = "";
            entryPermitted = false;
            if (StaticVariables.DebugEntry)
                Debug.Log("Vehicle Entry revoked");
        }
    }


    void EnterVehicle()
    {
        if (entryPermitted)
        {
            EventSystem.GetComponent<EventScripts>().playerIsInVehicle();
        }
    }
}
