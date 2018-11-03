using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {

    private static AppManager ins = null;

    public static AppManager Instance
    {
        get
        {
            if (null == ins)
                ins = new AppManager();

            return ins;
        }
    }

    // 델리게이트(대리자) 정의
    public delegate void __OnApplicationPause(bool pause);
    // 델리게이트(대리자) 변수
    public __OnApplicationPause OnPauseDelegate = null;

    void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    private void OnApplicationPause(bool pause)
    {
        if (null != OnPauseDelegate)
            OnPauseDelegate(pause);

        if (false == pause)
        {
            // 다시 들어올때 (foreground)
        }
        else
        {
            // 홈버튼 눌렀을때 (background)
        }

    }
}
