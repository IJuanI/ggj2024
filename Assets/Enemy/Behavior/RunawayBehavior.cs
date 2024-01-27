using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class RunawayBehavior : BehaviorBase
{

  [SerializeField]
  public float speed = 1f;

  public override void Start<T>(T enemy)
  {
    Vector2 target = enemy.scene.GetPointToRunaway(enemy.transform.position);

    enemy.transform.DOMove(target, speed).SetEase(Ease.Linear)
      .OnComplete(() => {
        Object.Destroy(enemy.gameObject);
      });
  }

  public override void Stop()
  {
  }

  public override void Update()
  {
  }
}
