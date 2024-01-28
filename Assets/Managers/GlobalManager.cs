using UnityEngine;


public class GlobalManager : MonoBehaviour {

  public static GlobalManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }

  public RunawayBehavior runawayBehavior;

  public Player player;


}