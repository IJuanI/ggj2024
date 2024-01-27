using System;
using UnityEngine;

[Serializable]
public abstract class BehaviorBase
{

    public abstract void Start<T>(T enemy) where T : MonoBehaviour, IEnemy;

    public abstract void Update();

    public abstract void Stop();

}
