using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum FireAction {
    Primary,
    Secondary
}

public class WeaponManager : MonoBehaviour
{

    public List<AnyGun> primaryWeapons;
    public List<AnyGun> secondaryWeapons;

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

    public void Shoot(InputAction.CallbackContext ctx)
    {

        if (ctx.phase == InputActionPhase.Started) return;

        FireAction action = ctx.action.name == "FirePrimary" ? FireAction.Primary : FireAction.Secondary;
        AnyGun weapon = getWeapon(action);

        if (ctx.canceled) {
            weapon?.CancelShoot();
            return;
        };
        //Debug.Log("paso por acaaaaaa-------------------------");
        UIManager.instance.Shoot(0);
        weapon?.Shoot(Vector2.zero);
    }

}
