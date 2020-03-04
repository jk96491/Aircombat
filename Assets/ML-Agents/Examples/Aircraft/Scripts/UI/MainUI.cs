using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private UIProgressBar hpProgress = null;
    [SerializeField]
    private UILabel hpLabel = null;

    public void SetHpInfo(int maxHP, int curHP)
    {
        if (null != hpLabel)
            hpLabel.text = string.Format("{0} / {1}", curHP, maxHP);

        float ratio = (float)curHP / (float)maxHP;

        if (null != hpProgress)
            hpProgress.value = ratio;
    }
}
