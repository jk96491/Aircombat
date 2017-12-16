using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPPLogin {

    public static void RequestLogin(string userID_, Action<string> SuccessCallback_, Action<int> FailCallback_)
    {
        userID = userID_;
        HandleOnSuccess = SuccessCallback_;
        HandleOnFail = FailCallback_;

        string TempUserId = PlayerPrefs.GetString(userID, null);
        string goldKey = string.Format("{0}:Gold", userID);
        string gemKey = string.Format("{0}:Gem", userID);
        UserLocal.UserMoney money = new UserLocal.UserMoney();

        if (true == string.IsNullOrEmpty(TempUserId))
        {
            PlayerPrefs.SetString(userID, userID);

            money.gold = 5000;
            money.gem = 80;
            PlayerPrefs.SetInt(goldKey, money.gold);
            PlayerPrefs.SetInt(gemKey, money.gem);
            
            //UIManager.Instance.OpenPopUp("회원가입 완료");
        }
        else
        {
            money.gold = PlayerPrefs.GetInt(goldKey, money.gold);
            money.gem = PlayerPrefs.GetInt(gemKey, money.gem);
        }
        UserManager.Instance.localUser.SetMoney(money);
        ResponseLogin(true);
    }

    public static void ResponseLogin(bool result_)
    {
        if(true == result_)
        {
            if(null != HandleOnSuccess)
            {
                HandleOnSuccess(userID);
            }
        }
        else
        {
            if (null != HandleOnFail)
            {
                HandleOnFail(-1);
            }
        }
    }

    private static string userID;
    private static Action<string> HandleOnSuccess;
    private static Action<int> HandleOnFail;
}
