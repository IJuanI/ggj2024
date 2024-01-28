using UnityEngine;
using UnityEngine.Events;


public class AnimateBehavior : BehaviorBase {

  [SerializeField] UnityEvent OnAnimate;
  [SerializeField] Animator animator;
  [SerializeField] AnimationClip animationClip;
  [SerializeField] bool loop = false;

  
  public override void Play<T>(T enemy)
  {
    if (animator == null) return;
    if (animationClip == null) return;

    animator.Play(animationClip.name, 0, 0f);
    OnAnimate?.Invoke();

    if (!loop) {
      Invoke("PublishEndBehavior", animationClip.length);
    }
  }

  public override void Stop() {
    animator.Play("Idle", 1, 0f);
  }

  public override void Update() { }
}