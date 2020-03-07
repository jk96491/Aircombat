using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private int enemyMaxCount = 8;
    [SerializeField]
    private List<Enemy> enemies = null;
    [SerializeField]
    private Transform[] enemyStartPosTrans = null;
    [SerializeField]
    private GameObject enemyPrefab = null;

    private float startPosZ = -6;
    private float intervalZ = 3;

    public int curEnemyCount = 0;

    public delegate void EnemyDie();
    public EnemyDie EnemyDieDel;

    public void Init()
    {
        enemies = new List<Enemy>();

        for(int i = 0; i < enemyMaxCount; i++)
        {
            GameObject ins = GameObject.Instantiate(enemyPrefab);

            if (null == ins)
                continue;

            Enemy EnemyIns = ins.GetComponent<Enemy>();

            if (null == EnemyIns)
                continue;

            ProjectileManager.Instance.HitBulletdel += EnemyIns.HitBulltEvent;

            float x = enemyStartPosTrans[i % 4].localPosition.x;
            EnemyIns.SetPostion(new Vector3(x, 3, startPosZ + (intervalZ * (i / 4))));

            EnemyIns.Init();
            enemies.Add(EnemyIns);
        }

        curEnemyCount = enemyMaxCount;
    }

    public void ResetEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy EnemyIns = enemies[i];

            if (null == EnemyIns)
                continue;

            float x = enemyStartPosTrans[i % 4].localPosition.x;
            EnemyIns.SetPostion(new Vector3(x, 3, startPosZ + (intervalZ * (i / 4))));

            EnemyIns.Init();
        }

        curEnemyCount = enemyMaxCount;
    }

    public void UpdateElapsed(float Elapsed_)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (false == enemies[i].gameObject.activeInHierarchy)
                continue;

            enemies[i].UpdateElapsed(Elapsed_);

            if(enemies[i].curHP <= 0)
            {
                enemies[i].gameObject.SetActive(false);

                curEnemyCount--;
                if (null != EnemyDieDel)
                    EnemyDieDel();

            }
        }
    }
}
