using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject prefabToSpawn;

    [SerializeField] float spawnDelay;

    public void Spawn(CurrentScene scene)
    {
        scene.RegisterEnemy(prefabToSpawn);
        Invoke(nameof(DoSpawn), spawnDelay);
    }

    void DoSpawn()
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }

}
