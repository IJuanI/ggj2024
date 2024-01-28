using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
struct PathEntry
{
    public GameObject target;
    public float startDelay;
}

[Serializable]
public class MoveBehavior : BehaviorBase
{
  [SerializeField] List<PathEntry> path;

  float speed = 1f, rotationSpeed = 2f;
  bool hasGun;

  Sequence sequence;

  public override void Init(dynamic data)
  {
    speed = data.speed;
    rotationSpeed = data.rotationSpeed;
    hasGun = data.hasGun;
  }

  public override void Play<T>(T enemy)
  {
    if (sequence == null)
    {
      sequence = DOTween.Sequence();
      sequence.SetUpdate(UpdateType.Fixed);

      sequence.AppendCallback(() => enemy.transform.position = path[0].target.transform.position);
      for (int i = 1; i < path.Count; i++)
      {
        sequence.AppendInterval(path[i].startDelay);
        Vector3 targetPosition = path[i].target.transform.position;
        
        Vector3 currentPos = path[i - 1].target.transform.position;
        float distance = Vector3.Distance(currentPos, targetPosition);
        sequence.Join(enemy.transform.DOMove(targetPosition, distance / speed / 4f));
        sequence.Join(enemy.transform.DOLookAt(targetPosition, 1f / rotationSpeed));
      }
      sequence.AppendInterval(path[0].startDelay);
      Vector3 firstTargetPosition = path[0].target.transform.position;
      Vector3 lastTargetPosition = path[path.Count - 1].target.transform.position;
      float lastDistance = Vector3.Distance(lastTargetPosition, firstTargetPosition);
      sequence.Join(enemy.transform.DOMove(firstTargetPosition, lastDistance / speed / 4f));
      sequence.Join(enemy.transform.DOLookAt(firstTargetPosition, 1f / rotationSpeed));

      sequence.SetLoops(-1);
    }

    sequence.Play();
    PublishMove(true);
  }

  public override void Stop()
  {
    sequence.Pause();
    PublishMove(false);
  }

  public override void Update() {}
}
