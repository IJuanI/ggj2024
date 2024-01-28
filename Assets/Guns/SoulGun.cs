using UnityEngine;

public class SoulGun : BaseGun<Soul>
{

  Soul targetSoul;

  SoulGun() : base(10, 0, -1, .5f, 2f) { }

  public override bool Shoot(Vector2 screenPos)
  {
    if (!isActivating) {
      if (GetTarget(screenPos, out targetSoul)) {
        return base.Shoot(targetSoul);
      }
    }

    return base.Shoot(screenPos);
  }

  public override void OnStartActivate()
  {
    if (targetSoul == null) {
      CancelShoot();
      return;
    }
  }
}
