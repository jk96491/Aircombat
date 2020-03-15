using UnityEngine;
using MLAgents;

public class AircraftAgent : Agent
{
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

    float flowTime = 0f;

    public bool isSetDelegateEvent = false;

    private Vector3 firstPos = Vector3.zero;

    public override void InitializeAgent()
    {
        if (null != trans)
            trans.localPosition = startPos;

        shootMaxCooltime = 0.3f;
        shootFlowCooltime = 0f;
        MaxHP = curHP;

        if(false == isSetDelegateEvent)
        {
            isSetDelegateEvent = true;

            ProjectileManager.Instance.HitBulletdel += HitBulltEvent;
            EnemyManager.Instance.EnemyDieDel += EnemyDieEvent;

            firstPos = trans.localPosition;
        }

        mainUI.SetHpInfo(MaxHP, curHP);
    }

    public override void CollectObservations()
    {
        AddVectorObs(moveX);
        AddVectorObs(moveZ);
        AddVectorObs(shoot);
        AddVectorObs(EnemyManager.Instance.curEnemyCount);
        AddVectorObs(curHP);
        AddVectorObs(trans.localPosition);
        AddVectorObs(flowTime);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        moveZ = Mathf.Clamp(vectorAction[0], -1f, 1f);
        moveX = Mathf.Clamp(vectorAction[1], -1f, 1f);

        shoot = Mathf.Clamp(vectorAction[2], -1f, 1f) >= 0;

        if (moveZ == 0 && moveX == 0)
            SetReward(-1);
        else
            SetReward(2f * flowTime);
    }

    public override void AgentReset()
    {
        shootMaxCooltime = 0.3f;
        shootFlowCooltime = 0f;
        curHP = MaxHP;
        flowTime = 0f;

        mainUI.SetHpInfo(MaxHP, curHP);
        ProjectileManager.Instance.ResetAllBullet();
        EnemyManager.Instance.ResetEnemy();

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
                ProjectileManager.Instance.ShootBullet(ProjectileManager.SHOOTER.PLAYER, fireHoleTrans.position);
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

        flowTime += Elapsed_;

        if (trans.position.x >= 3.85)
        {
            trans.position = new Vector3(3.85f, trans.position.y, trans.position.z);
            SetReward(-20f);
            Done();
        }
        if (trans.position.x <= -3.85)
        {
            trans.position = new Vector3(-3.85f, trans.position.y, trans.position.z);
            SetReward(-20f);
            Done();
        }
        if (trans.position.z >= 8)
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, 8);
            SetReward(-20f);
            Done();
        }
        if (trans.position.z <= 3)
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, 3);
            SetReward(-20f);
            Done();
        }

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
            SetReward(50f);
        }
    }

    public void EnemyDieEvent()
    {
        SetReward(50f);

        if(EnemyManager.Instance.curEnemyCount <= 0)
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
        SetReward((8 - EnemyManager.Instance.curEnemyCount) * 30f);
        Done();
    }
}
