using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScene : MonoBehaviour,ITriggereable
{

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    Timer timerForEndScene;
    [SerializeField] float timeToGoOff=0;

    [SerializeField] List<GameObject> runawaySpots = new List<GameObject>();

    [SerializeField] List<Spawner> spawners = new List<Spawner>();
    [SerializeField] bool infiniteTime = false;
    [SerializeField] bool finish = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerForEndScene != null)
        {
            //Debug.Log("timer");
            if (timerForEndScene.TimerStarted)
            {
                if (!infiniteTime&&!finish)
                {
                    Debug.Log("+++++++++++++++++++++++++++++++++++");
                    timerForEndScene.UpdateTimer();

                }
            }
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveElementsFromScene()
    {
        finish = true;
        Debug.Log("remove elements");
        foreach (GameObject enemy in enemies)
        {
            IEnemy inter;
            if(enemy.TryGetComponent<IEnemy>(out inter))
            {
                inter.Remove();
            }
        }
        DollyPathManager.instance.ResumeCart();
        DollyPathManager.instance.ResetCameraToFront();
        timerForEndScene.OnFinishTimer -= RemoveElementsFromScene;
    }

    public void StartScene()
    {
        Debug.Log("Start scene");
        timerForEndScene= new Timer(false, 0, timeToGoOff);
        timerForEndScene.OnFinishTimer += RemoveElementsFromScene;
        
        timerForEndScene.StartTimer(timeToGoOff);

        foreach (Spawner spawner in spawners)
            spawner.Spawn(this);
    }

    public void Trigger()
    {
        StartScene();
    }

    public Vector2 GetPointToRunaway(Vector3 pos) {
        float minDistance = float.MaxValue;
        GameObject closestSpot = null;
        foreach (var spot in runawaySpots) {
            var distance = Vector3.Distance(pos, spot.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                closestSpot = spot;
            }
        }

        return closestSpot.transform.position;
    }
}
