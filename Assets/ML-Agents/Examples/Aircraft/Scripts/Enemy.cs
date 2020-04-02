using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Direction : int
    {
        NONE = -1,
        LEFT,
        RIGHT,
    }
    
    private ProjectileManager projectileManager = null;
    [SerializeField]
    private Transform trans = null;
    [SerializeField]
    private Transform fireHoleTrans = null;

    private Direction curDir = Direction.LEFT;

    private float StartPosX = 0;

    int MaxHP = 30;
    public int curHP = 30;

    bool first = true;

    public void Init()
    {
        projectileManager = ProjectileManager.Instance;
        if (true == first)
        {
            first = false;
            StartPosX = trans.localPosition.x;
        }

        gameObject.SetActive(true);

        MaxHP = 30;
        curHP = MaxHP;

        curDir = Direction.RIGHT;
    }

    float fireMaxCoolTime = 1f;
    float coolTime = 0f;

    bool isCoolTime = false;

    public void UpdateElapsed(float Elapsed_)
    {
        if(null != trans)
        {
            float moveX = curDir == Direction.LEFT ? -1 : 1;
            trans.localPosition += Vector3.right * Elapsed_ * moveX;
        }

        if(Mathf.Abs(StartPosX - trans.localPosition.x) >= 1.16)
        {
            if (curDir == Direction.LEFT)
                curDir = Direction.RIGHT;
            else
                curDir = Direction.LEFT;
        }

        if(true == isCoolTime)
            coolTime += Elapsed_;

        float rand = Random.Range(0, 1000);

        if (rand < 10)
        {
            if (true == isCoolTime)
                return;

            isCoolTime = true;

            float rand2 = Random.Range(0, 100);

            if(rand2 > 10)
                projectileManager.ShootBullet(ProjectileManager.SHOOTER.ENEMY, fireHoleTrans.position);
            else
            {
                projectileManager.ShootBullet(ProjectileManager.SHOOTER.ENEMY, fireHoleTrans.position, -0.3f);
                projectileManager.ShootBullet(ProjectileManager.SHOOTER.ENEMY, fireHoleTrans.position, 0);
                projectileManager.ShootBullet(ProjectileManager.SHOOTER.ENEMY, fireHoleTrans.position, 0.3f);
            }
        }

        if(coolTime >= fireMaxCoolTime)
        {
            isCoolTime = false;
            coolTime = 0f;
        }
    }

    public void SetPostion(Vector3 pos_)
    {
        if (null == trans)
            trans = gameObject.transform;

        if (null != trans)
            trans.localPosition = pos_;
    }

    public void HitBulltEvent(ProjectileManager.SHOOTER shooter, GameObject obj)
    {
        if (shooter == ProjectileManager.SHOOTER.PLAYER)
        {
            if(obj == gameObject)
                curHP -= 6;
        }
    }

}
