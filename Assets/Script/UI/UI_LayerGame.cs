using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LayerGame : UI_LayerBase
{
    [SerializeField]
    private LeftJoystick joystick = null;
    [SerializeField]
    private Button fireButton = null;

    private PlaneBase player = null;

    public bool IsClickedFire = false;

    private void Update()
    {
        Fire();
    }

    // 초기화
    protected override void Initailize()
    {
        ProjectileManager.Instance.InitProjectile(); // 발사체 초기화

        if (null != joystick)
        {
            joystick.getInputvec = GetJoystickVec;
        }

        GameObject myPlanePrefab = Resources.Load(UserManager.Instance.localUser.GetCurrentPlane()) as GameObject;
        GameObject startPosObj = GameObject.Find("StartPos");
        GameObject plane = Instantiate(myPlanePrefab, startPosObj.transform.position, startPosObj.transform.rotation);

        if(null != plane)
        {
            player = plane.GetComponent<PlaneBase>();
        }
        
        if(null != fireButton)
        {
            fireButton.onClick.AddListener(Fire);
        }

        Enemy_Manager.Instance.InitEnemy();

        GameObject enemyPosObj = GameObject.Find("Enemy_Pos");
        Transform enemyPos1 = enemyPosObj.transform.FindChild("1");
        Transform enemyPos2 = enemyPosObj.transform.FindChild("2");
        Transform enemyPos3 = enemyPosObj.transform.FindChild("3");

        Enemy_Manager.Instance.CreateEnemy(enemyPos1.position);
        Enemy_Manager.Instance.CreateEnemy(enemyPos2.position);
        Enemy_Manager.Instance.CreateEnemy(enemyPos3.position);
    }

    private void FireEvent(PointerEventData env)
    {

    }

    public void Fire()
    {
        if (false == IsClickedFire)
            return;
        if(null != player)
            player.FireBullet();
    }

    protected override void Refresh()
    {

    }

    private void GetJoystickVec(Vector3 vec_)
    {
        if (null != player)
        {
            player.SetMoveVec(vec_);
        }
    }
    
    public void OnPressDownFire()
    {
        IsClickedFire = true;
    }

    public void OnPressUpFire()
    {
        IsClickedFire = false;
    }
}