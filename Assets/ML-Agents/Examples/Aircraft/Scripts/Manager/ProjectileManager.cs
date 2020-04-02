using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoSingleton<ProjectileManager>
{
    public enum SHOOTER : int
    {
        NONE = -1,
        PLAYER = 0,
        ENEMY = 1
    }

    [SerializeField]
    private int maxBulletCount = 0;
    [SerializeField]
    private List<Bullet> bullets = null;
    [SerializeField]
    private GameObject bulletPrefabs = null;

    public delegate void HitBullet(ProjectileManager.SHOOTER shooter, GameObject obj_);
    public HitBullet HitBulletdel;

    public void Init()
    {
        bullets = new List<Bullet>();

        for(int i = 0; i < maxBulletCount; i++)
        {
            GameObject ins = GameObject.Instantiate(bulletPrefabs);

            if (null == ins)
                continue;

            Bullet bulletScript = ins.GetComponent<Bullet>();

            if (null == bulletScript)
                continue;

            if(null == bulletScript.HitBulletdel)
                bulletScript.HitBulletdel += HitBulltEvent;

            bullets.Add(bulletScript);

            bulletScript.Init(ins);
        }
    }

    public void ResetAllBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].gameObject.SetActive(false);
        }
    }

    public void UpdateElapsed(float Elapsed_)
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            if (false == bullets[i].gameObject.activeInHierarchy)
                continue;

            bullets[i].UpdateElapsed(Elapsed_);
        }
    }

    public void ShootBullet(SHOOTER shooter, Vector3 firePos_, float xDir = 0f)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (true == bullets[i].gameObject.activeInHierarchy)
                continue;

            bullets[i].ShootBullet(shooter, firePos_, xDir);
            bullets[i].SetActive(true);
            break;
        }
    }

    public void HitBulltEvent(ProjectileManager.SHOOTER shooter, GameObject obj)
    {
        if (null != HitBulletdel)
            HitBulletdel(shooter, obj);
    }

}
