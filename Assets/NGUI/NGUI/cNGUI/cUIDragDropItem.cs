using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUIDragDropItem : UIDragDropItem
{
    protected override void OnDragDropStart()
    {
        if (!draggedItems.Contains(this))
            draggedItems.Add(this);

        // Automatically disable the scroll view
        if (mDragScrollView != null) mDragScrollView.enabled = false;

        // Disable the collider so that it doesn't intercept events
        if (mButton != null) mButton.isEnabled = false;
        else if (mCollider != null) mCollider.enabled = false;
        else if (mCollider2D != null) mCollider2D.enabled = false;

        mParent = mTrans.parent;
        mRoot = NGUITools.FindInParents<UIRoot>(mParent);
        mGrid = NGUITools.FindInParents<UIGrid>(mParent);
        mTable = NGUITools.FindInParents<UITable>(mParent);

        dragStart = true;

        // Re-parent the item
        if (UIDragDropRoot.root != null)
            mTrans.parent = UIDragDropRoot.root;

        Vector3 pos = mTrans.localPosition;
        pos.z = 0f;
        mTrans.localPosition = pos;

        TweenPosition tp = GetComponent<TweenPosition>();
        if (tp != null) tp.enabled = false;

        SpringPosition sp = GetComponent<SpringPosition>();
        if (sp != null) sp.enabled = false;

        // Notify the widgets that the parent has changed
        NGUITools.MarkParentAsChanged(gameObject);

        if (mTable != null) mTable.repositionNow = true;
        if (mGrid != null) mGrid.repositionNow = true;
    }


    protected override void Update()
    {
        if (restriction == Restriction.PressAndHold)
        {
            if (mPressed && !mDragging && mDragStartTime < RealTime.time)
                StartDragging();
        }

        if (dragStart)
        {
            if (mTrans.position.y > .4f)
            {
                downAccel += 0.001f;
            }
            else if (mTrans.position.y < -.9f)
            {
                upAccel += 0.001f;
            }
            else
            {
                downAccel = 0;
                upAccel = 0;
            }
        }
    }

}
