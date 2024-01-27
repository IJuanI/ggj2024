using UnityEngine;

public class Clown : MonoBehaviour, IEnemy, IStunnable
{

  [SerializeField]
  BehaviorBase behavior;

  [SerializeField]
  float health = 30f;

  [SerializeField]
  float stunDuration = 3f;

  [HideInInspector]
  public CurrentSceneDisposer scene { get; private set; }

  void OnStart() {
    behavior.Start(this);
  }

  void Update() {
    behavior.Update();
  }

  public void Remove()
  {
    Die();
  }

  public void SetScene(CurrentSceneDisposer scene) {
    this.scene = scene;
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
    behavior.Start(this);
  }


  void Die() {
    behavior.Stop();

    behavior = new RunawayBehavior();
    behavior.Start(this);
  }
}
