using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ProjectileManager : MonoBehaviour {

    const int MAX_PROJECTILE = 50;

    private Bullet[] bullet = new Bullet[MAX_PROJECTILE];

    static ProjectileManager _instance = new ProjectileManager();

    public static ProjectileManager Instance
    {
        get
        {
            return _instance;
        }
    }


    public void InitProjectile()
    {
        GameObject ProjectilePrefab = Resources.Load("Bullet") as GameObject;

        for(int index = 0; index < MAX_PROJECTILE; index++)
        {
            GameObject Projectile = Instantiate(ProjectilePrefab);

            if(null != Projectile)
            {
                bullet[index] = Projectile.GetComponent<Bullet>();
                Projectile.SetActive(false);
            }
        }
    }

    public void CreateBullet(Vector3 vec_, Vector3 firePos_)
    {
        for (int index = 0; index < MAX_PROJECTILE; index++)
        {
            if(null != bullet[index])
            {
                if(bullet[index].gameObject.activeInHierarchy == false)
                {
                    bullet[index].gameObject.SetActive(true);
                    bullet[index].SetPos(firePos_);
                    bullet[index].SetMoveVec(vec_);
                    break;
                }
            }
        }
    }
}
