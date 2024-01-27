using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public enum Scores {
        score = 0,
        kills = 0,
        firedBullets = 0,
        headshots = 0,
        shotsHit = 0,
        shotsFail = 0
    }
    private Dictionary<Scores, int> scores = new Dictionary<Scores, int>();
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }
    public void OnAwake () {

        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // public void OnStart()
    // {

    // }

    public void AddScore(Scores scoretype, int pound = 1)
    {
        if (scoretype == Scores.score){
            scores[scoretype] += 1 * pound;
        } else {
            scores[scoretype] += 1;
        }
    }
    
    // [ContextMenu("Testing")]
    // public void Testing()
    // {
    //     Debug.Log("prueba!");
    // }
}