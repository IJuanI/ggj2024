using UnityEngine;

public interface IGun
{

    public abstract void Shoot(Vector2 screenPos);

    public abstract void CancelShoot();
}
