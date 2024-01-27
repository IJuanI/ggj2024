using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DollyPathManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] CinemachineVirtualCamera cinemachineCameraToLook;
    [SerializeField] CinemachineDollyCart cartReference;
    [SerializeField] Transform cartFront;
    [SerializeField] float cartSpeed;
    [SerializeField] Transform currentTargetForCamera;
    [SerializeField] int mainPriority=10;
    [SerializeField] int secondaryPriority=5;

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
    public void SetTargetForCamera(Transform transformToLook)
    {
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
    }
    public void StopCart()
    {
        cartReference.m_Speed = 0;
    }

}
