using System;
using UnityEngine;

public abstract class AnyGun : MonoBehaviour
{

    public event Action OnReload;

    public abstract void Shoot(Vector2 screenPos);

    public abstract void CancelShoot();

    protected void InvokeOnReload() {
        OnReload?.Invoke();
    }
}
