using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LayerLogin : UI_LayerBase {
    [SerializeField]
    private Button LoginButton = null;
    [SerializeField]
    private InputField LoginInput = null;

    private void Start()
    {
        if(null != LoginButton)
        {
            LoginButton.onClick.AddListener(HandleOnClickLoginButton);
        }
    }

    private void HandleOnClickLoginButton()
    {
        if(true == string.IsNullOrEmpty(LoginInput.text))
        {
            return;
        }

        UserManager.Instance.localUser.RequsetLogin(LoginInput.text, HandleOnLoginSucess);
    }

    private void HandleOnLoginSucess()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
