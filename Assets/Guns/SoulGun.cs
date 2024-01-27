using UnityEngine;

public class SoulGun : BaseGun<Soul>
{

  Vector2 initialTargetPos;

  SoulGun() : base(10, 0, -1, .5f, 2f) { }

  public override void Shoot(Vector2 screenPos)
  {
    if (!isActivating) {
      initialTargetPos = screenPos;
    }

    base.Shoot(initialTargetPos);
  }

  public override void OnStartActivate()
  {
    if (!GetTarget(initialTargetPos, out _)) {
      CancelShoot();
      return;
    }
  }
}
