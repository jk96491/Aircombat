using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    [SerializeField]
    private AircraftAgent agent = null;

    public delegate void TimeOutDel();
    public TimeOutDel timeOutDel;

    private void Awake()
    {
        ProjectileManager.Instance.Init();
        EnemyManager.Instance.Init();
        timeOutDel += agent.TimeOut;
    }

    float gameTime = 40;

    public void Update()
    {
        float time = Time.deltaTime * 3f;
        agent.UpdateElapesd(time);

        ProjectileManager.Instance.UpdateElapsed(time);
        EnemyManager.Instance.UpdateElapsed(time);

        gameTime -= time;

        if(gameTime <= 0)
        {
            gameTime = 30;

            if(null != timeOutDel)
                timeOutDel();
        }
    }
}
