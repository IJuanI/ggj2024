using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BulletUIHolder : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float timeToDissapear= 0.3f;

    public void Shrink()
    {
        rectTransform.DOSizeDelta(new Vector2(rectTransform.sizeDelta.x, 0), timeToDissapear);
        //rectTransform.DOScaleY(0,timeToDissapear-0.02f);
        Selfdestruct();
    }

    void Selfdestruct()
    {
        StartCoroutine(SelfDestruct());        
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToDissapear+0.01f);
    }

}
