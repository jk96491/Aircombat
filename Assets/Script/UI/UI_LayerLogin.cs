using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LayerLogin : UI_LayerBase {
    [SerializeField]
    private UIButton LoginButton = null;
    [SerializeField]
    private UIInput LoginInput = null;

    private void Start()
    {
        if(null != LoginButton)
        {
            LoginButton.onClick.Add(new EventDelegate(HandleOnClickLoginButton));
        }
    }

    private void HandleOnClickLoginButton()
    {
        if(true == string.IsNullOrEmpty(LoginInput.value))
        {
            return;
        }

        UserManager.Instance.localUser.RequsetLogin(LoginInput.value, HandleOnLoginSucess);
    }

    private void HandleOnLoginSucess()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
