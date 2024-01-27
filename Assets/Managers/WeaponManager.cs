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

    public List<IGun> primaryWeapons;
    public List<IGun> secondaryWeapons;

    private IGun getWeapon(FireAction action)
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

        if (ctx.started) return;

        FireAction action = ctx.action.name == "FirePrimary" ? FireAction.Primary : FireAction.Secondary;
        IGun weapon = getWeapon(action);

        if (ctx.canceled) {
            weapon?.CancelShoot();
            return;
        };

        weapon?.Shoot(ctx.ReadValue<Vector2>());
    }

}
