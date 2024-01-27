using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum FireAction {
    Primary,
    Secondary
}


public class WeaponManager : MonoBehaviour
{

    public List<AnyGun> primaryWeapons;
    public List<AnyGun> secondaryWeapons;

    public UnityEvent<FireAction> OnShootEvent;
    public UnityEvent<FireAction> OnReloadEvent;

    Dictionary<FireAction, bool> actionStates = new Dictionary<FireAction, bool>() {
        { FireAction.Primary, false },
        { FireAction.Secondary, false }
    };

    private AnyGun getWeapon(FireAction action)
    {
        switch (action)
        {
            case FireAction.Primary:
                return primaryWeapons[0];
            case FireAction.Secondary:
                return secondaryWeapons[0];
            default:
                Debug.Log("Unknown fire action");
                return null;
        }
    }

    void Start() {
        foreach (var weapon in primaryWeapons)
            weapon.OnReload += () => OnReloadEvent?.Invoke(FireAction.Primary);

        foreach (var weapon in secondaryWeapons)
            weapon.OnReload += () => OnReloadEvent?.Invoke(FireAction.Secondary);
    }

    void Update() {

        foreach (var actionState in actionStates)
        {
            if (actionState.Value) {
                getWeapon(actionState.Key)?.Shoot(Mouse.current.position.ReadValue());
            }
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) return;

        FireAction action = ctx.action.name == "PrimaryFire" ? FireAction.Primary : FireAction.Secondary;
        AnyGun weapon = getWeapon(action);

        if (ctx.canceled) {
            weapon?.CancelShoot();
            actionStates[action] = false;
            return;
        };

        if (actionStates[action]) return;
        actionStates[action] = true;

        OnShootEvent?.Invoke(action);
        weapon?.Shoot(Mouse.current.position.ReadValue());
    }

}
