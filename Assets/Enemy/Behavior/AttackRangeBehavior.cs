using UnityEngine;

public class AttackRangeBehavior : BehaviorBase {

  [SerializeField] AnyGun weapon;

  bool active = false;
  
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

    if (weapon.Shoot(GlobalManager.instance.player.transform.position)) {
      PublishAttack();
    }
  }
}