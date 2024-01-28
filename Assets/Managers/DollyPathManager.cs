using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DollyPathManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] CinemachineVirtualCamera cinemachineCameraToLook;
    [SerializeField] CinemachineBrain brain1;
    [SerializeField] CinemachineBrain brain2;
    [SerializeField] CinemachineDollyCart cartReference;
    [SerializeField] Transform cartFront;
    [SerializeField] float cartSpeed;
    [SerializeField] Transform currentTargetForCamera;
    [SerializeField] int mainPriority=10;
    [SerializeField] int secondaryPriority=5;

    [SerializeField] GameObject canvasWeapon1;
    public static DollyPathManager instance; 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
        
    }
    private void Start()
    {
        brain1.enabled = true;
        brain2.enabled = true;
    }
    public void SetTargetForCamera(Transform transformToLook)
    {
        //canvasWeapon1.transform.parent = cinemachineCameraToLook.transform;
        currentTargetForCamera = transformToLook;
        cinemachineCameraToLook.LookAt = currentTargetForCamera;
        cinemachineCamera.Priority = secondaryPriority;
        cinemachineCameraToLook.Priority = mainPriority;
    }
    public void ResetCameraToFront()
    {
        cinemachineCameraToLook.Priority = secondaryPriority;
        cinemachineCamera.Priority = mainPriority;
        currentTargetForCamera = cartFront;
        cinemachineCameraToLook.LookAt=currentTargetForCamera;
        
        //canvasWeapon1.transform.parent = cinemachineCamera.transform;

    }
    public void StopCart()
    {
        cartReference.m_Speed = 0;
    }
    public void ResumeCart()
    {
        cartReference.m_Speed = 3;
    }


}
