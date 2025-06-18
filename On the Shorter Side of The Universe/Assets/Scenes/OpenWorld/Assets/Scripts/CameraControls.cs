using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Camera FpsCamera, VehicleCamera;
    [SerializeField] private AudioListener FpsSource, VehicleSource;
    [SerializeField] private GameObject EventSystem;

    void Start()
    {
        EventSystem.GetComponent<EventScripts>().playerIsInVehicleEvent += SwitchCameraToVehicle;
        SwitchCameraToFPS();
    }
    private void SwitchCameraToVehicle()
    {
        FpsCamera.enabled = false;
        FpsSource.enabled = false;
        VehicleCamera.enabled = true;
        VehicleSource.enabled = true;

    }

     private void SwitchCameraToFPS()
    {
        FpsCamera.enabled = true;
        FpsSource.enabled = true;
        VehicleCamera.enabled = false;
        VehicleSource.enabled = false;

    }
}
