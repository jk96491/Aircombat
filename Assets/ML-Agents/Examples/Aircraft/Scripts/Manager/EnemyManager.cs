using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private ProjectileManager projectileManager = null;
    [SerializeField]
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

            Enemy script = ins.GetComponent<Enemy>();

            if (null == script)
                continue;

            projectileManager.HitBulletdel += script.HitBulltEvent;

            float x = enemyStartPosTrans[i % 4].localPosition.x;
            script.SetPostion(new Vector3(x, 3, startPosZ + (intervalZ * (i / 4))));

            script.Init(projectileManager);
            enemies.Add(script);
        }

        curEnemyCount = enemyMaxCount;
    }

    public void ResetEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy script = enemies[i];

            if (null == script)
                continue;

            float x = enemyStartPosTrans[i % 4].localPosition.x;
            script.SetPostion(new Vector3(x, 3, startPosZ + (intervalZ * (i / 4))));

            script.Init(projectileManager);
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
