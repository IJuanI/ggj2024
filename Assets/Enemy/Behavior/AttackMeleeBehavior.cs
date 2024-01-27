public class AttackMeleeBehavior : BehaviorBase {

  bool active = false;
  
  public override void Start<T>(T enemy)
  {
    active = true;
  }

  public override void Stop()
  {
    active = false;
  }

  public override void Update()
  {
    if (!active) return;

    
  }
}