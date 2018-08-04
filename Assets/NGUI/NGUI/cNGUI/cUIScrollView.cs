using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUIScrollView : UIScrollView
{
    public UIPanel UIPanel_Parent;
    protected override void OnEnable()
    {
        base.OnEnable();
        if (UIPanel_Parent.depth >= mPanel.depth)
            mPanel.depth = UIPanel_Parent.depth + 1;
    }


    public bool CompareSizeY(float y)
    {
        if (panel.baseClipRegion.w <= y)
            return true;
        return false;
    }

    public UIGrid mScrollGrid;
    public GameObject mScrollItem;

    public Transform mTransGroupItems;
    public List<Transform> mlistScrollItemObjectPool = new List<Transform>();
    public List<Transform> mlistScrollItemGrid = new List<Transform>();

   
   

    public void AddItem()
    {
        Transform item = null;
        if (mlistScrollItemObjectPool.Count < 1)
        {
            GameObject newClone = GameObject.Instantiate(mScrollItem, mTransGroupItems) as GameObject;
            item = newClone.transform;
            item.localScale = Vector3.one;
        }
        else
        {
            item = mlistScrollItemObjectPool[0];
            mlistScrollItemObjectPool.RemoveAt(0);
        }
        mlistScrollItemGrid.Add(item);
        item.parent = mScrollGrid.transform;
        item.gameObject.SetActive(true);
        mScrollGrid.AddChild(item.transform);
    }
    public void RemoveItem(int index_)
    {
        Transform item = mlistScrollItemGrid[index_];
        mlistScrollItemGrid.RemoveAt(index_);
        item.parent = mTransGroupItems;
        item.gameObject.SetActive(false);
        mlistScrollItemObjectPool.Add(item);

        //mScrollGrid.Reposition();
        //Press(false);
    }




}