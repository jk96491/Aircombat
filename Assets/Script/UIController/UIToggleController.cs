using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleController : MonoBehaviour
{
    [SerializeField]
    private UIToggle[] toggles = null;

    public delegate void SetClickToggle(int clickIndex_);
    public SetClickToggle clickToggleDel = null;

    public void ResetToggle()
    {
        if (null != toggles)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (null != toggles[i])
                    toggles[i].value = false;
            }

            toggles[0].value = true;

            if (null != clickToggleDel)
                clickToggleDel(0);
        }
    }

    public void DisableAllToggle()
    {
        if (null != toggles)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (null != toggles[i])
                    toggles[i].value = false;
            }
        }
    }

    public void InitToggles()
    {
        if (null != toggles)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (null != toggles[i])
                {
                    UIEventListener.Get(toggles[i].gameObject).onClick = HandleOnClickToggles;
                }
            }
        }
    }

    private void HandleOnClickToggles(GameObject obj_)
    {
        int clickIndex = -1;

        if (null != toggles)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (null != toggles[i])
                {
                    toggles[i].value = false;
                    if (toggles[i].gameObject == obj_)
                    {
                        clickIndex = i;
                    }
                }
            }
        }

        if (clickIndex != -1)
        {
            if (null != toggles[clickIndex])
            {
                toggles[clickIndex].value = true;
                if (null != clickToggleDel)
                    clickToggleDel(clickIndex);
            }
        }
    }

}
