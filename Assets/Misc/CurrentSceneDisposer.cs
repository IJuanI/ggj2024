using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneDisposer : MonoBehaviour,ITriggereable
{

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    Timer timerForEndScene;
    [SerializeField] float timeToGoOff;

    [SerializeField] List<GameObject> runawaySpots = new List<GameObject>();

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerForEndScene != null)
        {
            Debug.Log("timer");
            if (timerForEndScene.TimerStarted)
            {
                timerForEndScene.UpdateTimer();
            }
        }
    }
    public void RemoveElementsFromScene()
    {
        Debug.Log("remove elements");
        foreach (GameObject enemy in enemies)
        {
            IEnemy inter;
            if(enemy.TryGetComponent<IEnemy>(out inter))
            {
                inter.Remove();
            }
        }
        DollyPathManager.instance.ResetCameraToFront();
        timerForEndScene.OnFinishTimer -= RemoveElementsFromScene;
    }

    public void StartScene()
    {
        Debug.Log("Start scene");
        timerForEndScene= new Timer(false, 0, timeToGoOff);
        timerForEndScene.OnFinishTimer += RemoveElementsFromScene;
        timerForEndScene.StartTimer();
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
