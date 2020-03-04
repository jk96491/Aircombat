using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager = null;
    [SerializeField]
    private ProjectileManager projectileManager = null;
    [SerializeField]
    private AircraftAgent agent = null;

    public delegate void TimeOutDel();
    public TimeOutDel timeOutDel;

    private void Awake()
    {
        projectileManager.Init();
        enemyManager.Init();
        timeOutDel += agent.TimeOut;
    }

    float gameTime = 30;

    public void Update()
    {
        float time = Time.deltaTime * 3f;
        agent.UpdateElapesd(time);
        projectileManager.UpdateElapsed(time);
        enemyManager.UpdateElapsed(time);

        gameTime -= time;

        if(gameTime <= 0)
        {
            gameTime = 30;
            if(null != timeOutDel)
                timeOutDel();
        }
    }
    

}
