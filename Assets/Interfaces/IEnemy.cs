using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IEnemy : IDamageable
{
    public CurrentScene scene { get; }

    public void Remove();

    public void SetScene(CurrentScene scene);
}
