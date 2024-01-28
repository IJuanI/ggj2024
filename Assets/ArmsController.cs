using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmsController : MonoBehaviour
{

    [SerializeField] Animator leftArm;
    [SerializeField] Animator rightArm;

    void Awake()
    {
        leftArm.SetBool("LeftArm", true);
    }

    public void Shoot(FireAction action) {
        if (action == FireAction.Primary) {
            leftArm.SetTrigger("Shoot");
        } else {
            rightArm.SetTrigger("Shoot");
        }
    }

    public void StartReload(FireAction action) {
        if (action == FireAction.Primary) {
            leftArm.SetBool("Reloading", true);
        } else {
            rightArm.SetBool("Reloading", true);
        }
    }

    public void StopReload(FireAction action) {
        if (action == FireAction.Primary) {
            leftArm.SetBool("Reloading", false);
        } else {
            rightArm.SetBool("Reloading", false);
        }
    }
}
