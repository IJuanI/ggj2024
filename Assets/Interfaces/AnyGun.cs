using System;
using UnityEngine;

public abstract class AnyGun : MonoBehaviour
{

    public event Action OnReload;

    public abstract bool Shoot(Vector2 screenPos);

    public abstract bool Shoot(IDamageable target);

    public abstract void CancelShoot();

    protected void InvokeOnReload() {
        OnReload?.Invoke();
    }
}
