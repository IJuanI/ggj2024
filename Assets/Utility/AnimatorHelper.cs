using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHelper : MonoBehaviour {

  private Animator animator;

  void Awake() {
    animator = GetComponent<Animator>();
  }

  public void SetBoolTrue(string name) {
    animator.SetBool(name, true);
  }

  public void SetBoolFalse(string name) {
    animator.SetBool(name, false);
  }

  public void ToggleBool(string name) {
    animator.SetBool(name, !animator.GetBool(name));
  }
}