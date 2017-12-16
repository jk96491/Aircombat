using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour {

    static Enemy_Manager _instance = new Enemy_Manager();

    public static Enemy_Manager Instance
    {
        get
        {
            return _instance;
        }
    }

    const int MAX_ENEMY = 10;

    private Dictionary<int/*ID*/, string /*path*/> enemyPlanesDic = new Dictionary<int, string>();

    private PlaneBase[] enemys = new PlaneBase[MAX_ENEMY];

    public void InitEnemy()
    {
        enemyPlanesDic[0] = "Blue";
        enemyPlanesDic[2] = "Green";
        enemyPlanesDic[1] = "Red";

        for (int i = 0; i < enemys.Length; i++ )
        {
            GameObject planePrefab = Resources.Load(enemyPlanesDic[(int)Random.Range(0,2)]) as GameObject;

            GameObject plane = Instantiate(planePrefab);

            enemys[i] = plane.GetComponent<PlaneBase>();

            plane.SetActive(false);
        }
    }

    public void CreateEnemy(Vector3 Pos_)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if(null != enemys[i])
            {
                if (true == enemys[i].gameObject.activeInHierarchy)
                    continue;
                enemys[i].gameObject.SetActive(true);
                enemys[i].SetPostion(Pos_);
                break;
            }
        }
    }
}
