using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LayerLobby : UI_LayerBase {
    [SerializeField]
    private UIButton startBtn = null;
    [SerializeField]
    private UIButton selectPlaneBtn = null;
    [SerializeField]
    private UILabel goldValue = null;
    [SerializeField]
    private UILabel gemValue = null;
    [SerializeField]
    private UILabel NicknameValue = null;

    protected override void Initailize()
    {
        if (null != startBtn)
        {
            startBtn.onClick.Add(new EventDelegate( HandleOnClickStartButton));
        }

        if (null != selectPlaneBtn)
        {
            selectPlaneBtn.onClick.Add(new EventDelegate(HandleOnClickSelectPlaneButton));
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
