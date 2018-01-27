using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBase : MonoBehaviour {

    public Vector3 moveVec = Vector3.zero;

    [SerializeField]
    private float MoveSpeed = 0f;
    [SerializeField]
    private Transform myTrans = null;
    [SerializeField]
    private Rigidbody myRigid = null;

    [SerializeField]
    private GameObject firePos = null;

    [SerializeField]
    private Vector3 firstRotation = Vector3.zero;

    private bool IsFired = false;

    // 임시코드
    private void Update()
    {
        UpdatePlane();
    }

    public void UpdatePlane()
    {
        if(null != myTrans)
        {
            myTrans.Translate(moveVec * MoveSpeed * Time.deltaTime);

            myTrans.rotation = Quaternion.Euler(firstRotation);
        }
    }

    public void SetMoveVec(Vector3 moveVec_)
    {
        //moveVec = moveVec_;

        moveVec.x = moveVec_.x;
        moveVec.z = moveVec_.y;
    }

    public void FireBullet()
    {
        if(false == IsFired)
            StartCoroutine(FireShot());
    }

    IEnumerator FireShot()
    {
        IsFired = true;
        //ProjectileManager.Instance.CreateBullet(myTrans.forward, firePos.transform.position);
        yield return new WaitForSeconds(0.05f);

        IsFired = false;
    }
    
    public void SetPostion(Vector3 Pos_)
    {
        myTrans.position = Pos_;
    }
}
