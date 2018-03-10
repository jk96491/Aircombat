using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ViewBase : MonoBehaviour
{
    [SerializeField]
    private UIButton button = null;
    [SerializeField]
    private UITexture image = null;

    public int dataIndex = -1;

    public Action<UI_ViewBase> onClick = null;

    public virtual void HandleOnClickButton()
    {
        if(null != onClick)
        {
            onClick(this);
        }
    }

    public void SetImage(Texture image_)
    {
        if(null != image)
            image.mainTexture = image_;
    }
} 


