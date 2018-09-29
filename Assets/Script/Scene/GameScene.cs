using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectType : int
{
    none = -1,
    Rock,
    Scissor,
    Paper
}

public class GameScene : MonoBehaviour {
    [SerializeField]
    private UIToggleController SelectController = null;
    
    private SelectType UserSelect = SelectType.none;
    private SelectType ComSelect = SelectType.none;
    private bool IsSelectUser = false;

    [Header("[유저관련]")]
    [SerializeField]
    private GameObject BlockObj = null;
    [SerializeField]
    private UISpriteAnimation UserAni = null;
    [SerializeField]
    private UISprite UserSprite = null;

    [Header("[컴퓨터 관련]")]
    [SerializeField]
    private UISpriteAnimation ComAni = null;
    [SerializeField]
    private UISprite ComSprite = null;

    // Use this for initialization
    void Start ()
    {
        if(SelectController != null)
        {
            SelectController.InitToggles();
            if (null == SelectController.clickToggleDel)
                SelectController.clickToggleDel = HandleOnSelect;
        }

        SetBlock(false);
        SetAniInfo(UserAni, true);
        SetAniInfo(ComAni, true);
    }

    public void HandleOnSelect(int clickIndex)
    {
        if (false == IsSelectUser)
        {
            IsSelectUser = true;
            SetBlock(true);
            SetAniInfo(UserAni, false);
            SetAniInfo(ComAni, false);
        }
        else
        {
            return;
        }

        UserSelect = (SelectType)clickIndex;    // 강제 형변환 
        SetSprite(UserSprite, UserSelect);
        ComSelecting();
    }
	
    private void SetBlock(bool Block)
    {
        if(null != BlockObj)
        {
            BlockObj.SetActive(Block);
        }
    }

    private void SetAniInfo(UISpriteAnimation Ani, bool Play)
    {
        if(Ani != null)
        {
            if(Play == true)
                Ani.Play();
            else
                Ani.Pause();
        }
    }

    private void SetSprite(UISprite Sprite, SelectType Type)
    {
        if(Sprite == null)
            return;

        switch (Type)
        {
            case SelectType.Rock:
                {
                    Sprite.spriteName = "RockSprite";
                }
                break;
            case SelectType.Scissor:
                {
                    Sprite.spriteName = "ScissorsSprite";
                }
                break;
            case SelectType.Paper:
                {
                    Sprite.spriteName = "PaperSprite";
                }
                break;
        }
    }
    
    private void ComSelecting()
    {
        ComSelect = (SelectType)Random.Range(0, 3);
        SetSprite(ComSprite, ComSelect);
        Debug.LogError(ComSelect);

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);

        SetBlock(false);
        SetAniInfo(UserAni, true);
        SetAniInfo(ComAni, true);

        UserSelect = SelectType.none;
        ComSelect = SelectType.none;
        IsSelectUser = false;
        if (SelectController != null)
            SelectController.DisableAllToggle();

    }
}
