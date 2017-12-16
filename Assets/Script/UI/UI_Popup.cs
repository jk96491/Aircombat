using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Popup : UI_LayerBase {

    [SerializeField]
    private Text message = null;
    [SerializeField]
    private Button okBtn = null;

    protected override void Initailize()
    {
        if(null != message)
        {
            message.text = string.Empty;
        }
        if(null != okBtn)
        {
            okBtn.onClick.AddListener(HandleOnClickOkButton);
        }
    }

    public void SetMessage(string msg_)
    {
        message.text = msg_;
    }

    private void HandleOnClickOkButton()
    {
        UIManager.Instance.ClosePopupUI();
    }
}
