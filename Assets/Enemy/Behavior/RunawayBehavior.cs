using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class RunawayBehavior : BehaviorBase
{

  float speed = 1f;

  public override void Init(dynamic data)
  {
    speed = data.speed;
  }

  public override void Play<T>(T enemy)
  {
    if (enemy.scene == null) return;
    Vector2 target = enemy.scene.GetPointToRunaway(enemy.transform.position);

    enemy.transform.DOMove(target, speed).SetEase(Ease.Linear)
      .OnComplete(() => {
        Destroy(enemy.gameObject);
      });
  }

  public override void Stop()
  {
  }

  public override void Update()
  {
  }
}
