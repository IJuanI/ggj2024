using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyCartTrigger : MonoBehaviour
{
    [SerializeField] bool stopOnTrigger = true;
    [SerializeField] Transform newTargetToLook;
    [SerializeField] GameObject triggereable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        if (DollyPathManager.instance != null)
        {
            //Debug.Log("paso por aca");
            DollyPathManager.instance.SetTargetForCamera(newTargetToLook);
            if (stopOnTrigger)
            {
                DollyPathManager.instance.StopCart();
            }
            if (triggereable != null)
            {
                ITriggereable iTrigger;
                if(triggereable.TryGetComponent<ITriggereable>(out iTrigger))
                {
                    iTrigger.Trigger();

                }
            }
        }
    }
    


}
