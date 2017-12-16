using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ViewBase : MonoBehaviour
{
    [SerializeField]
    private Button button = null;
    [SerializeField]
    private Image image = null;

    protected Action onClick = null;

    protected virtual void HandleOnClickButton()
    {
        if(null != onClick)
        {
            onClick();
        }
    }
} 


