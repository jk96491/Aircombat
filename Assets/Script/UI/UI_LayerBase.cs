using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayerBase : MonoBehaviour
{
    public void InitUI()
    {
        Initailize();
    }

    protected virtual void Initailize()
    {

    }

    protected virtual void Refresh()
    {

    }

    public void RefreshUI()
    {
        Refresh();
    }

    protected virtual void DeActivate()
    {

    }

    public void DeActiveUI()
    {
        DeActivate();
    }
}   

    

