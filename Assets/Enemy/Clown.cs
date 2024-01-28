using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public struct MaybeAlterAnimation {
  [SerializeField] public string altParam;
  [SerializeField] public float chance;
}

[Serializable]
public struct BehaviorSettings {
  [SerializeField] public BehaviorBase behavior;
  [SerializeField] public float duration;
}

public class Clown : MonoBehaviour, IEnemy, IStunnable
{
  
  [Header("Enemy Stats")]

  [SerializeField] float health = 9f;

  [SerializeField] float stunDuration = 3f;

  [SerializeField] float walkSpeed = 1f;
  [SerializeField] float rotationSpeed = 1f;

  [SerializeField] float escapeSpeed = 1f;

  [SerializeField] bool hasGun = false;

  [Header("Behaviors")]
  [SerializeField] List<BehaviorSettings> behaviors = new List<BehaviorSettings>();
  [SerializeField] bool runaway = true;

  [Header("Animations")]

  [SerializeField] List<MaybeAlterAnimation> alterAnimations = new List<MaybeAlterAnimation>();

  public CurrentScene scene { get { return _scene; } }

  [SerializeField] UnityEvent onStun;
  [SerializeField] UnityEvent onUnstun;
  [SerializeField] UnityEvent onDie;
  [SerializeField] UnityEvent onEscape;
  [SerializeField] UnityEvent onStartMoving;
  [SerializeField] UnityEvent onStopMoving;
  [SerializeField] UnityEvent onAttack;
  [SerializeField] UnityEvent onSwitchAttack;
  [SerializeField] UnityEvent onStartDancing;
  [SerializeField] UnityEvent onStopDancing;


  [SerializeField]
  CurrentScene _scene;

  private BehaviorBase runawayBehavior;

  private bool dead;

  private int currentBehavior = 0;

  private BehaviorBase behavior;
  
  private Animator animator;


  void Start() {
    runawayBehavior = GetComponent<RunawayBehavior>();
    behavior = behaviors[currentBehavior = 0].behavior;

    RegisterBehaviorCallbacks(behavior);
    RegisterBehaviorCallbacks(runawayBehavior);

    behavior.Play(this);

    animator = GetComponentInChildren<Animator>();
    foreach (MaybeAlterAnimation alterAnimation in alterAnimations) {
      if (UnityEngine.Random.Range(0f, 1f) < alterAnimation.chance) {
        animator.SetBool(alterAnimation.altParam, true);
      }
    }

    animator.SetBool("Range Atk", behaviors[behaviors.Count - 1].behavior is AttackRangeBehavior);
    animator.SetBool("Has Gun", hasGun);

    if (behaviors[currentBehavior].duration > 0) {
      Invoke(nameof(NextBehavior), behaviors[currentBehavior].duration);
    }
  }

  void Update() {
    behavior.Update();
  }

  void RegisterBehaviorCallbacks(BehaviorBase behavior) {
    behavior.OnMove += BroadcastMove;
    behavior.OnAttack += BroadcastAttack;
    behavior.OnDance += BroadcastDance;
    behavior.OnEndBehavior += NextBehavior;
  }

  void NextBehavior() {
    behavior.Stop();

    currentBehavior = Math.Min(currentBehavior + 1, behaviors.Count - 1);
    behavior = behaviors[currentBehavior].behavior;

    dynamic initData = new {
      speed = walkSpeed,
      hasGun,
      target = GlobalManager.instance.player,
      rotationSpeed
    };
    behavior.Init(initData);

    RegisterBehaviorCallbacks(behavior);
    behavior.Play(this);

    if (behaviors[currentBehavior].duration > 0) {
      Invoke(nameof(NextBehavior), behaviors[currentBehavior].duration);
    }
  }

  public void Remove()
  {
    behavior.Stop();
    
    if (dead || !runaway) {
      Destroy(gameObject);
    } else {
      behavior = Instantiate(GlobalManager.instance.runawayBehavior);
      behavior.Init(new { speed = escapeSpeed });
      behavior.Play(this);
      onEscape.Invoke();
    }
  }

  public void SetScene(CurrentScene scene) {
    _scene = scene;
  }

  public void TakeDamage(float damage)
  {
    health -= damage;
    if (health <= 0) {
      Die();
    }
  }

  public void Stun()
  {
    behavior.Stop();
    onStun.Invoke();
    Invoke(nameof(ResumeBehavior), stunDuration);
  }

  void BroadcastMove(bool moving) {
    if (moving) {
      onStartMoving.Invoke();
    } else {
      onStopMoving.Invoke();
    }
  }

  void BroadcastAttack() {
    if (UnityEngine.Random.Range(0f, 1f) < 0.5f) {
      onSwitchAttack.Invoke();
    }

    onAttack.Invoke();
  }

  void BroadcastDance(bool dance) {
    if (dance) {
      onStartDancing.Invoke();
    } else {
      onStopDancing.Invoke();
    }
  }

  void ResumeBehavior() {
    behavior.Play(this);
    onUnstun.Invoke();
  }


  void Die() {
    behavior.Stop();
    dead = true;

    onDie.Invoke();
  }
}
