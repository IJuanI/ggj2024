using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeakPoint: MonoBehaviour, IDamageable {


  public void TakeDamage(float damage)
  {
    GetComponentInParent<IStunnable>()?.Stun();
    GetComponentInParent<IDamageable>()?.TakeDamage(damage * 1.5f);

    Die();
  }

  void Die() {
    Destroy(gameObject);
  }

  
}