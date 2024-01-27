using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] WeaponIndicator weaponIndicator0;
    [SerializeField] WeaponIndicator weaponIndicator1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(int weapon)
    {
        switch (weapon)
        {
            case 0:
                Debug.Log("shoot");
                weaponIndicator0.SpendBullet();
                break;
            case 1:
                weaponIndicator1.SpendBullet();

                break;
        }
    }
    public void Reload(int weapon,int bulletsToReload)
    {
        switch (weapon)
        {
            case 0:
                Debug.Log("shoot");
                weaponIndicator0.Reload(bulletsToReload);
                break;
            case 1:
                weaponIndicator1.Reload(bulletsToReload);
                break;
        }
    }
}
