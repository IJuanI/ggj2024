using System;
using UnityEngine;

[Serializable]
public abstract class BehaviorBase : MonoBehaviour
{

    public abstract void Play<T>(T enemy) where T : MonoBehaviour, IEnemy;

    public abstract void Update();

    public abstract void Stop();

}
