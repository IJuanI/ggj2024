using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIndicator : MonoBehaviour
{
    [SerializeField] GameObject bulletHolderUIPrefab;
    [SerializeField] Transform bulletHolderContainer;
    [SerializeField] List<BulletUIHolder> listOfBulletsHolders;
    // Start is called before the first frame update
    void Start()
    {
        Clean();
        FillBullets(6);
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpendBullet();
        }*/
    }
    public void FillBullets(int bullets)
    {
        for(int i = 0; i < bullets; i++)
        {
            GameObject bulletHolder = Instantiate(bulletHolderUIPrefab,bulletHolderContainer);
            listOfBulletsHolders.Add( bulletHolder.GetComponent<BulletUIHolder>());
        }
    }
    [ContextMenu("spend menu")]
    public void SpendBullet()
    {
        if (listOfBulletsHolders.Count > 0)
        {
            listOfBulletsHolders[0].Shrink();
            listOfBulletsHolders.RemoveAt(0);
        }
    }
    public void Reload( int reload)
    {
        Clean();
        FillBullets(reload);
    }
    void Clean()
    {
        for(int i= bulletHolderContainer.childCount-1; i > -1; i--)
        {
            Destroy(bulletHolderContainer.GetChild(i).gameObject);
        }
    }
}
