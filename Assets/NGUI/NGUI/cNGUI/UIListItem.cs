using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListItemBase
{
	public ListItemBase Prev;
	public ListItemBase Next;
	
	public ListItemBase()
	{
		Prev = Next = null;
	}
}

public class UIListItem : ListItemBase
{

    public int Index;
    public GameObject Target;

    public UIListItem()
    {
        Index = -1;
        Target = null;
    }

    public void SetIndex(int index)
    {
        if (Index != index)
        {
            Index = index;
            //if (Target != null)
            //{
            //    cUIScrollListBase scr = Target.GetComponent<cUIScrollListBase>();
            //    Debug.LogError(scr);
            //    scr.ListItem = this;
            //}
        }
    }
}

//public class cUIScrollListBase : MonoBehaviour
//{
//    public UIListItem ListItem;
//}
