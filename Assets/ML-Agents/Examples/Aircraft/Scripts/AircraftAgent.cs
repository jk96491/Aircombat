using UnityEngine;
using MLAgents;

public class AircraftAgent : Agent
{
    [SerializeField]
    private ProjectileManager projectileManager = null;
    [SerializeField]
    private EnemyManager enemyManager = null;
    [SerializeField]
    private Transform trans = null;
    [SerializeField]
    private Vector3 startPos = Vector3.zero;
    [SerializeField]
    private Transform fireHoleTrans = null;
    [SerializeField]
    private MainUI mainUI = null;

    float moveX = 0f;
    float moveZ = 0f;
    bool shoot = false;
    bool isCooltime = false;
    float shootFlowCooltime = 0f;
    float shootMaxCooltime = 0f;

    int MaxHP = 100;
    int curHP = 100;
    public bool setDel = false;

    private Vector3 firstPos = Vector3.zero;

    public override void InitializeAgent()
    {
        if (null != trans)
            trans.localPosition = startPos;

        shootMaxCooltime = 0.3f;
        shootFlowCooltime = 0f;
        MaxHP = curHP;

        if(false == setDel)
        {
            setDel = true;
            projectileManager.HitBulletdel += HitBulltEvent;
            enemyManager.EnemyDieDel += EnemyDieEvent;

            firstPos = trans.localPosition;
        }

        mainUI.SetHpInfo(MaxHP, curHP);
    }

    public override void CollectObservations()
    {
        AddVectorObs(moveX);
        AddVectorObs(moveZ);
        AddVectorObs(shoot);
        AddVectorObs(enemyManager.curEnemyCount);
        AddVectorObs(curHP);
        AddVectorObs(trans.localPosition);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        moveZ = Mathf.Clamp(vectorAction[0], -1f, 1f);
        moveX = Mathf.Clamp(vectorAction[1], -1f, 1f);

        shoot = Mathf.Clamp(vectorAction[2], -1f, 1f) >= 0;

        if (moveZ == 0 && moveX == 0)
            SetReward(-1);
    }

    public override void AgentReset()
    {
        shootMaxCooltime = 0.3f;
        shootFlowCooltime = 0f;
        curHP = MaxHP;

        mainUI.SetHpInfo(MaxHP, curHP);
        projectileManager.ResetAllBullet();
        enemyManager.ResetEnemy();

        trans.localPosition = firstPos;
    }

    public override void AgentOnDone()
    {

    }

    public void UpdateElapesd(float Elapsed_)
    {
        SetMovePosition(Elapsed_);

        if(false == isCooltime)
        {
            if (true == shoot)
            {
                isCooltime = true;
                projectileManager.ShootBullet(ProjectileManager.SHOOTER.PLAYER, fireHoleTrans.position);
                SetReward(1f);
            }
        }
        else
        {
            shootFlowCooltime += Elapsed_;

            if(shootFlowCooltime >= shootMaxCooltime)
            {
                shootFlowCooltime = 0f;
                isCooltime = false;
            }
        }

        if(trans.position.x >= 3.85)
            trans.position = new Vector3(3.85f, trans.position.y, trans.position.z);
        if (trans.position.x <= -3.85)
            trans.position = new Vector3(-3.85f, trans.position.y, trans.position.z);
        if (trans.position.z >= 8)
            trans.position = new Vector3(trans.position.x, trans.position.y, 8);
        if (trans.position.z <= 3)
            trans.position = new Vector3(trans.position.x, trans.position.y, 3);

    }

    public void SetMovePosition(float Elapsed_)
    {
        if (null != trans)
            trans.localPosition += new Vector3(moveX, 0, moveZ) * Elapsed_ * 1.5f;
    }

    public void HitBulltEvent(ProjectileManager.SHOOTER shooter, GameObject obj)
    {
        if(shooter == ProjectileManager.SHOOTER.ENEMY)
        {
            curHP -= 5;
            SetReward(-40f);

            mainUI.SetHpInfo(MaxHP, curHP);

            if (curHP <= 0f)
            {
                SetReward(-100f);
                Done();
            }
        }

        if (shooter == ProjectileManager.SHOOTER.PLAYER)
        {
            SetReward(10f);
        }
    }

    public void EnemyDieEvent()
    {
        SetReward(50f);

        if(enemyManager.curEnemyCount <= 0)
        {
            SetReward(100f);
            Done();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("wall"))
        {
            SetReward(-5f);
        }
    }

    public void TimeOut()
    {
        SetReward(-100f);
        Done();
    }
}
