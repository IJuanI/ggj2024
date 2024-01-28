using UnityEngine;

public class IdleBehavior : BehaviorBase {

  [SerializeField] float startDancingTimeout = 3f;
  [SerializeField] float stopDancingTimeout = 5f;

  float startTime = -1f;
  bool dancing = false;
  
  public override void Play<T>(T enemy)
  {
    if (startTime == -1f) {
      startTime = Time.time;
    }
  }

  public override void Stop()
  {
  }

  public override void Update()
  {
    if (startTime == -1f) return;

    if (Time.time - startTime > startDancingTimeout && !dancing) {
      dancing = true;
      startTime = Time.time;
      PublishDance(true);
    }

    if (Time.time - startTime > stopDancingTimeout && dancing) {
      dancing = false;
      startTime = Time.time;
      PublishDance(false);
    }
  }
}