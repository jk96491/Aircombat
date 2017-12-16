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

    public int dataIndex = -1;

    public Action<UI_ViewBase> onClick = null;

    protected virtual void HandleOnClickButton()
    {
        if(null != onClick)
        {
            onClick(this);
        }
    }

    public void SetImage(Sprite image_)
    {
        if(null != image)
            image.sprite = image_;
    }
} 


