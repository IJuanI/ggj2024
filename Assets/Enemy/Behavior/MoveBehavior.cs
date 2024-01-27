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

  public override void Play<T>(T enemy)
  {
    if (sequence == null) {
      sequence = DOTween.Sequence();
      sequence.SetUpdate(UpdateType.Fixed);
      
      sequence.AppendCallback(() => enemy.transform.position = path[0].transform.position);
      for (int i = 1; i < path.Count; i++) {
        sequence.Append(enemy.transform.DOMove(path[i].transform.position, speed));
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