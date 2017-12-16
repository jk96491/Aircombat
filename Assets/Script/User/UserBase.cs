using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserBase : MonoBehaviour {

    protected string userID = string.Empty;
    protected string nickname = string.Empty;
    private Action success = null;

    public string UserId { get { return userID; } }

    
    public void RequsetLogin(string inputID_, Action successCallback)
    {
        success = successCallback;
        NPPLogin.RequestLogin(inputID_, HandleOnSucessLogin, HandleOnFailLogin);
    }

    private void HandleOnSucessLogin(string UserID_)
    {
        userID = UserID_;
        Debug.Log("로그인 성공 : "+ userID);

        if (null != success)
        {
            success();
        }
    }
    private void HandleOnFailLogin(int error_)
    {
        UIManager.Instance.OpenPopUp("로그인 실패!!!");
    }
}
