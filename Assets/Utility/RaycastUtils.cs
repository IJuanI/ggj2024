using UnityEngine;

public class RaycastUtils
{

  static public bool RaycastFromCamera(out RaycastHit hit, Vector2 screenPos, int layerMask = Physics.DefaultRaycastLayers)
  {
    Ray ray = Camera.main.ScreenPointToRay(screenPos);
    return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
  }
}
