using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public enum SLOT_TYPE
    {
        NONE = -1,
        CHARACTER_SLOT,
        CHARACTER_LIST
    }

    public int dataIndex = -1;
    public SLOT_TYPE slotType = SLOT_TYPE.NONE;
    public int viewIndex = -1;

    [SerializeField]
    private GameObject viewObj = null;
    
    public delegate void OnClickDelegate(UIViewBase viewBase_);
    public OnClickDelegate onClick;
    public delegate void OnDragStartDelegate(UIViewBase viewBase_);
    public OnDragStartDelegate onDragStart;
    public delegate void OnDropDelegate(UIViewBase dropBase_, UIViewBase dragBase_);
    public OnDropDelegate onDrop;
    public delegate void OnDragEndDelegate(UIViewBase viewBase_);
    public OnDragEndDelegate onDragEnd;

    protected virtual void OnClick()
    {
        if(null != onClick)
            onClick(this);
    }

    protected virtual void OnDragStart()
    {
        if (null != onDragStart)
            onDragStart(this);
    }

    protected virtual void OnDrop(GameObject go_)
    {
        UIViewBase destBase = go_.GetComponent<UIViewBase>();

        if (null != onDrop)
            onDrop(this, destBase);
    }

    protected virtual void OnDragEnd()
    {
        if (null != onDragEnd)
            onDragEnd(this);
    }

    public void SetActiveViewObj(bool active_)
    {
        if (null != viewObj)
            viewObj.SetActive(active_);
    }
}
