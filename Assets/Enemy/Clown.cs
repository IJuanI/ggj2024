using UnityEngine;

[RequireComponent(typeof(RunawayBehavior))]
public class Clown : MonoBehaviour, IEnemy, IStunnable
{

  [SerializeField]
  BehaviorBase behavior;

  private BehaviorBase runawayBehavior;

  [SerializeField]
  float health = 9f;

  [SerializeField]
  float stunDuration = 3f;

  public CurrentScene scene { get { return _scene; } }

  [SerializeField]
  CurrentScene _scene;

  void Start() {
    behavior.Play(this);
    runawayBehavior = GetComponent<RunawayBehavior>();
  }

  void Update() {
    behavior.Update();
  }

  public void Remove()
  {
    Die();
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
    Invoke(nameof(ResumeBehavior), stunDuration);
  }

  void ResumeBehavior() {
    behavior.Play(this);
  }


  void Die() {
    behavior.Stop();

    behavior = runawayBehavior;
    behavior.Play(this);
  }
}
