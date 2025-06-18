using System;
using UnityEngine;
using UnityEngine.AI;

public class EventScripts : MonoBehaviour
{
    //I think this is just useless
    public EventScripts singletoninstance;

    private void Awake()
    {
        singletoninstance = this;
    }

    public event Action playerIsOnFootEvent;
    public event Action playerIsInVehicleEvent;
    public event Action playerRequestedActionEvent;

    public void playerIsOnFoot()
    {
        if (playerIsOnFootEvent != null)
        {
            playerIsOnFootEvent();

            if (StaticVariables.DebugActions)
                Debug.Log("PlayerIsOnFoot");
        }
    }

    public void playerIsInVehicle()
    {
        if (playerIsInVehicleEvent != null)
        {
            playerIsInVehicleEvent();

            if (StaticVariables.DebugActions)
                Debug.Log("PlayerIsInVehicle");
        }
    }

    public void playerRequestedAction()
    {
        if (playerRequestedActionEvent != null)
        {
            playerRequestedActionEvent();

            if (StaticVariables.DebugActions)
                Debug.Log("PlayerRequestedAction");
        }

    }
}
