using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class MoveBehavior : BehaviorBase
{
  [SerializeField]
  List<GameObject> path;

  [SerializeField]
  float speed = 1f;

  Sequence sequence;

  public override void Start<T>(T enemy)
  {
    if (sequence == null) {
      sequence = DOTween.Sequence();
      sequence.SetUpdate(UpdateType.Fixed);
      foreach (var point in path) {
        sequence.Append(enemy.transform.DOMove(point.transform.position, speed));
      }
      sequence.Append(enemy.transform.DOMove(path[0].transform.position, speed));

      sequence.SetLoops(-1);
    }

    sequence.Play();
  }

  public override void Stop()
  {
    sequence.Pause();
  }

  public override void Update() { }
}