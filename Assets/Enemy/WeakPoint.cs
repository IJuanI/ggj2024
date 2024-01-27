using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeakPoint: MonoBehaviour, IDamageable {


  public void TakeDamage(float _)
  {
    GetComponentInParent<IStunnable>()?.Stun();

    Die();
  }

  void Die() {
    Destroy(gameObject);
  }

  
}