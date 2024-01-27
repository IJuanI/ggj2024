using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EntityController : MonoBehaviour,IDamageable
{
    [SerializeField] Transform placeToGo;
    [SerializeField] Vector3 originalPosition;
    Sequence mySequence;
    [SerializeField] float velocityOfMovement;
    [SerializeField] Ease ease;
    [SerializeField] bool movementOnStart = false;


    [SerializeField] float life = 1;
    public event Action onDeathEvent;
    [SerializeField] bool spawnObjectOnDeath = true;
    [SerializeField] GameObject ObjectToSpawnOnDeath;
    private void Awake()
    {
        originalPosition = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (movementOnStart&&placeToGo!=null)
        {
            GoToPlace();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GoToPlace()
    {
        mySequence = DOTween.Sequence();
        mySequence.SetUpdate(UpdateType.Fixed);
        mySequence.Append(this.transform.DOMove(placeToGo.position,velocityOfMovement).SetEase(ease));
        mySequence.Play();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            onDeathEvent?.Invoke();
        }
        
    }

    public void SpawnObjectOnDeath()
    {
        GameObject obj = Instantiate(ObjectToSpawnOnDeath, this.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.parent = null;
    }
}
