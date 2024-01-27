using UnityEngine;

public abstract class AnyGun : MonoBehaviour
{

    public abstract void Shoot(Vector2 screenPos);

    public abstract void CancelShoot();
}
