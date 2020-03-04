using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject obj = null;
    private Transform trans = null;

    public float moveZ = 0f;
    private float bulletSpeed = 2f;
    public ProjectileManager.SHOOTER shooter = ProjectileManager.SHOOTER.NONE;

    [SerializeField]
    private MeshRenderer renderer = null;
    [SerializeField]
    private Material mat1 = null;
    [SerializeField]
    private Material mat2 = null;

    public delegate void HitBullet(ProjectileManager.SHOOTER shooter, GameObject obj);
    public HitBullet HitBulletdel;

    public void Init(GameObject obj_)
    {
        obj = obj_;
        SetActive(false);
        trans = gameObject.transform;
        renderer.material = mat1;
    }

    public void SetActive(bool active_)
    {
        if (null != obj)
            obj.SetActive(active_);
    }

    public void SetPosition(Vector3 pos_)
    {
        if (null != trans)
            trans.localPosition = pos_;
    }

    public void UpdateElapsed(float Elapsed_)
    {
        if (null != trans)
            trans.localPosition += Vector3.forward * Elapsed_ * bulletSpeed * moveZ;
    }

    public void ShootBullet(ProjectileManager.SHOOTER shooter_, Vector3 firePos_)
    {
        shooter = shooter_;
        SetPosition(firePos_);
        moveZ = shooter == ProjectileManager.SHOOTER.PLAYER ? -1 : 1;

        if(null != renderer)
            renderer.material = shooter == ProjectileManager.SHOOTER.PLAYER ? mat1 : mat2;
    }

    private void OnTriggerEnter(Collider col_)
    {
        if(col_.CompareTag("wall"))
        {
            moveZ = 0;
            SetActive(false);
            shooter = ProjectileManager.SHOOTER.NONE;
        }
        else
        {
            if (col_.CompareTag("enemy"))
            {
                if (shooter == ProjectileManager.SHOOTER.PLAYER)
                {
                    moveZ = 0;
                    SetActive(false);

                    if (null != HitBulletdel)
                        HitBulletdel(shooter, col_.gameObject);
                }
            }

            if (col_.CompareTag("player"))
            {
                if (shooter == ProjectileManager.SHOOTER.ENEMY)
                {
                    moveZ = 0;
                    SetActive(false);
                    if (null != HitBulletdel)
                        HitBulletdel(shooter, col_.gameObject);
                }
            }
        }
    }
}
