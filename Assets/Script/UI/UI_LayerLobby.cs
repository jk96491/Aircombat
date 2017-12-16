using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LayerLobby : UI_LayerBase {
    [SerializeField]
    private Button startBtn = null;
    [SerializeField]
    private Button selectPlaneBtn = null;
    [SerializeField]
    private Text goldValue = null;
    [SerializeField]
    private Text gemValue = null;
    [SerializeField]
    private Text NicknameValue = null;

    protected override void Initailize()
    {
        if (null != startBtn)
        {
            startBtn.onClick.AddListener(HandleOnClickStartButton);
        }

        if (null != selectPlaneBtn)
        {
            selectPlaneBtn.onClick.AddListener(HandleOnClickSelectPlaneButton);
        }

        if (null != goldValue)
        {
            goldValue.text = UserManager.Instance.localUser.money.gold.ToString();
        }

        if (null != gemValue)
        {
            gemValue.text = UserManager.Instance.localUser.money.gem.ToString();
        }

        if (null != NicknameValue)
        {
            NicknameValue.text = UserManager.Instance.localUser.UserId;
        }
    }

    private void HandleOnClickStartButton()
    {
        SceneManager.LoadScene("CountryScene");
    }

    private void HandleOnClickSelectPlaneButton()
    {
        UIManager.Instance.OpenUI(UIManager.UIType.UI_SELECT_PLANE);
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LOBBY);
    }

}
