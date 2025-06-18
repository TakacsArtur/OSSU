using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAppearAndDisappear : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject EventSystem;
    void Start()
    {
        EventSystem.GetComponent<EventScripts>().playerIsInVehicleEvent += PlayerInVehicle;
    }

    void PlayerInVehicle()
    {
        transform.position = new Vector3(0, -10000, 0);
    }
}
