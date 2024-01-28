using UnityEngine;

public class AttackMeleeBehavior : BehaviorBase
{
  [SerializeField] AnyGun weapon;
  [SerializeField] float attackRange = 2f; // Range within which the entity can attack

  float speed = 5f;
  float rotationSpeed = 2f;
  bool active = false;

  IDamageable target; // Target to attack
  Transform targetTransform;

  public override void Init(dynamic data) {
    speed = data.speed;
    rotationSpeed = data.rotationSpeed;
    target = data.target;
    targetTransform = data.target.transform;
  }

  public override void Play<T>(T enemy)
  {
      active = true;
  }

  public override void Stop()
  {
      active = false;
  }

  public override void Update()
  {
    if (!active) return;

    float distance = Vector3.Distance(transform.position, targetTransform.position);

    // Only move until within 75% of the attack range
    bool move = distance > attackRange * 0.75f;
    GoToTarget(move);
    PublishMove(move);

    // Check if within attack range and attack
    if (distance <= attackRange)
    {
        if (weapon.Shoot(target))
        {
            PublishAttack();
        }
    }
  }

  private void GoToTarget(bool move)
  {
    Vector3 direction = (targetTransform.position - transform.position).normalized;

    if (move) {
      transform.position += direction * speed * Time.deltaTime;
    }

    Quaternion lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
  }
}
