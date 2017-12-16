using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Transform myTrans = null;
    [SerializeField]
    private float MoveSpeed = 0f;

    private Vector3 moveVec = Vector3.zero;

    //  임시코드
    private void Update()
    {
        UpdateBullet();
    }

    public void UpdateBullet()
    {
        if (null != myTrans)
        {
            myTrans.Translate(moveVec * MoveSpeed * Time.deltaTime);
        }
    }

    public void SetMoveVec(Vector3 moveVec_)
    {
        moveVec.x = -moveVec_.z;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            moveVec = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    public void SetPos(Vector3 pos_)
    {
        if (null != myTrans)
        {
            myTrans.position = pos_;
        }
    }
}
