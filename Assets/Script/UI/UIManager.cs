﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public enum UIType {
        UI_LOGIN,
        UI_LOBBY,
        UI_GAME,
        UI_SELECT_PLANE,
        UI_PopUp
    }
    
    private Dictionary<UIType/*UI_Type*/, UI_LayerBase> loadedUI_Dic = new Dictionary<UIType, UI_LayerBase>();
    [SerializeField]
    private UIRoot root = null;

    public void OpenUI(UIType type_)
    {
        GameObject uiPrefab = null;
        GameObject uiObj = null;
        UI_LayerBase ui_layer = null;

        if(true == loadedUI_Dic.ContainsKey(type_))
        {
            ui_layer = loadedUI_Dic[type_];
            ui_layer.RefreshUI();
            ui_layer.gameObject.SetActive(true);
            return;
        }
        else
        {
            //로드
            switch (type_)
            {
                case UIType.UI_LOGIN:
                    uiPrefab = Resources.Load("UI/UI_LayerLogin") as GameObject;
                    break;
                case UIType.UI_LOBBY:
                    uiPrefab = Resources.Load("UI/UI_Lobby") as GameObject;
                    break;
                case UIType.UI_GAME:
                    uiPrefab = Resources.Load("UI/UI_LayerGame") as GameObject;
                    break;
                case UIType.UI_SELECT_PLANE:
                    uiPrefab = Resources.Load("UI/UI_SelectPlane") as GameObject;
                    break;
            }

            uiObj = Instantiate(uiPrefab);
            ui_layer = uiObj.GetComponent<UI_LayerBase>();
            loadedUI_Dic.Add(type_, ui_layer);
            uiObj.transform.SetParent(root.transform);
            uiObj.GetComponent<Transform>().localPosition = Vector3.zero;
            uiObj.GetComponent<Transform>().localScale = Vector3.one;
            ui_layer.InitUI();
        }
    }

    public void OpenPopUp(string msg_)
    {
        GameObject uiPrefab = null;
        GameObject uiObj = null;
        UI_LayerBase ui_layer = null;
        UI_Popup popupUI;

        if (true == loadedUI_Dic.ContainsKey(UIType.UI_PopUp))
        {
            // 활성화
            ui_layer = loadedUI_Dic[UIType.UI_PopUp];
            ui_layer.gameObject.SetActive(true);
            popupUI = ui_layer as UI_Popup;
            ui_layer.InitUI();
            popupUI.SetMessage(msg_);
            return;
        }
        else
        {
            uiPrefab = Resources.Load("UI/UI_Popup") as GameObject;
            uiObj = Instantiate(uiPrefab);
            ui_layer = uiObj.GetComponent<UI_LayerBase>();
        }
        GameObject canvas = GameObject.Find("Canvas");
        uiObj.transform.SetParent(canvas.transform);
        uiObj.GetComponent<RectTransform>().localPosition = Vector3.zero;
        loadedUI_Dic.Add(UIType.UI_PopUp, ui_layer);
        ui_layer.InitUI();
        popupUI = ui_layer as UI_Popup;
        popupUI.SetMessage(msg_);
    }

    public void CloseUI(UIType type_)
    {
        UI_LayerBase uiLayer = null;

        if (true == loadedUI_Dic.ContainsKey(type_))
        {
            uiLayer = loadedUI_Dic[type_];
        }

        if(null != uiLayer)
        {
            uiLayer.DeActiveUI();
            uiLayer.gameObject.SetActive(false);
        }
    }

    public void ClosePopupUI()
    {
        UI_LayerBase uiLayer = null;

        if (true == loadedUI_Dic.ContainsKey(UIType.UI_PopUp))
        {
            uiLayer = loadedUI_Dic[UIType.UI_PopUp];
        }

        uiLayer.gameObject.SetActive(false);
    }
}

