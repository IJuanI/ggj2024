using System;
using UnityEngine;

[Serializable]
public abstract class BehaviorBase : MonoBehaviour
{

    public event Action<bool> OnMove;
    public event Action OnAttack;
    public event Action<bool> OnDance;
    public event Action OnEndBehavior;

    public virtual void Init(dynamic data) { }

    public abstract void Play<T>(T enemy) where T : MonoBehaviour, IEnemy;

    public abstract void Update();

    public abstract void Stop();

    protected void PublishMove(bool moving)
    {
        OnMove?.Invoke(moving);
    }

    protected void PublishAttack()
    {
        OnAttack?.Invoke();
    }

    protected void PublishDance(bool dance)
    {
        OnDance?.Invoke(dance);
    }

    protected void PublishEndBehavior()
    {
        OnEndBehavior?.Invoke();
    }
}
