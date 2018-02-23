using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUILabel : UILabel
{
    public string TEXT_KEY = string.Empty;
    public bool FLAG_ADD = false;
    private string TEXT_Add = string.Empty;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnLocalize();
    }
    protected override void OnStart()
    {
        base.OnStart();
        OnLocalize();
    }

    public void SetText(string key_)
    {
        TEXT_KEY = key_;
        OnLocalize();
    }
    public void AddText(string text_)
    {
        FLAG_ADD = true;
        TEXT_Add = text_;
    }

    void OnLocalize()
    {
      
    }
    
}
