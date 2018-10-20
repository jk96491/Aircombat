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

public enum ResultType : int
{
    none = -1,
    Win,
    Draw,
    Lose
}


public class GameScene : MonoBehaviour {
    [SerializeField]
    private UIToggleController SelectController = null;
    [SerializeField]
    private UILabel RecordLabel = null;
    [SerializeField]
    private GameObject VsObj = null;
    
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

    [Header("[전적 관련]")]
    [SerializeField]
    private int WinCount = 0;
    [SerializeField]
    private int LoseCount = 0;
    [SerializeField]
    private int DrawCount = 0;
    [SerializeField]
    private UILabel WinLabel = null;
    [SerializeField]
    private UILabel LoseLabel = null;
    [SerializeField]
    private UILabel DrawLabel = null;
    [SerializeField]
    private TweenPosition WinTweenPos = null;
    [SerializeField]
    private TweenPosition LoseTweenPos = null;
    [SerializeField]
    private TweenPosition DrawTweenPos = null;


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
        SetResultLabel();
        ResetResultObjs();
    }

    private void SetResultLabel()
    {
        if (null != WinLabel)
            WinLabel.text = Localization.Get("10");

        if (null != LoseLabel)
            LoseLabel.text = Localization.Get("11");

        if (null != DrawLabel)
            DrawLabel.text = Localization.Get("12");
    }

    private void ResetResultObjs()
    {
        if (null != WinLabel)
        {
            WinLabel.gameObject.SetActive(false);
            WinLabel.transform.localPosition = Vector3.zero;
        }

        if (null != LoseLabel)
        {
            LoseLabel.gameObject.SetActive(false);
            LoseLabel.transform.localPosition = Vector3.zero;
        }
            
        if (null != DrawLabel)
        {
            DrawLabel.gameObject.SetActive(false);
            DrawLabel.transform.localPosition = Vector3.zero;
        }
            
    }

    private void ActiveResultObj(ResultType type)
    {
        switch (type)
        {
            case ResultType.Win:
                {
                    if (null != WinLabel)
                        WinLabel.gameObject.SetActive(true);
                    if (null != WinTweenPos)
                    {
                        WinTweenPos.ResetToBeginning();
                        WinTweenPos.PlayForward();
                    }
                }
                break;

            case ResultType.Lose:
                {
                    if (null != LoseLabel)
                        LoseLabel.gameObject.SetActive(true);
                    if (null != LoseTweenPos)
                    {
                        LoseTweenPos.ResetToBeginning();
                        LoseTweenPos.PlayForward();
                    }
                }
                break;

            case ResultType.Draw:
                {
                    if (null != DrawLabel)
                        DrawLabel.gameObject.SetActive(true);
                    if (null != DrawTweenPos)
                    {
                        DrawTweenPos.ResetToBeginning();
                        DrawTweenPos.PlayForward();
                    }
                }
                break;
        }

    }

    private void OnEnable()
    {
        SetRecordLabel();
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
        ComSelect = (SelectType)(Random.Range(0, 99999999) % 3);
        SetSprite(ComSprite, ComSelect);

        ResultType result = GetResult();
        Debug.LogError(result);
        SetRecord(result);
        ActiveResultObj(result);
        SetActiveVsObj(false);
        SetRecordLabel();

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
        ResetResultObjs();
        SetActiveVsObj(true);
    }

    private ResultType GetResultMine()
    {
        ResultType result = ResultType.none;

        if (UserSelect == ComSelect)
            result = ResultType.Draw;
        else
        {
            // 보를 낼 때
            if(UserSelect == SelectType.Paper)
            {
                if (ComSelect == SelectType.Rock)
                    result = ResultType.Win;
                else
                    result = ResultType.Lose;
            }

            // 가위를 낼 때
            else if (UserSelect == SelectType.Scissor)
            {
                if (ComSelect == SelectType.Paper)
                    result = ResultType.Win;
                else
                    result = ResultType.Lose;
            }

            // 바위를 낼 때
            else if (UserSelect == SelectType.Rock)
            {
                if (ComSelect == SelectType.Scissor)
                    result = ResultType.Win;
                else
                    result = ResultType.Lose;
            }
        }

        return result;
    }

    private ResultType GetResult()
    {
        ResultType result = ResultType.none;

        if (UserSelect == ComSelect)
            result = ResultType.Draw;
        else
        {
            switch (UserSelect)
            {
                case SelectType.Paper: result = ComSelect == SelectType.Rock ? ResultType.Win : ResultType.Lose; break;
                case SelectType.Rock: result = ComSelect == SelectType.Scissor ? ResultType.Win : ResultType.Lose; break;
                case SelectType.Scissor: result = ComSelect == SelectType.Paper ? ResultType.Win : ResultType.Lose; break;
            }
        }

        return result;
    }

    private void SetRecord(ResultType Type)
    {
        switch (Type)
        {
            case ResultType.Win: WinCount++; break;
            case ResultType.Lose: LoseCount++; break;
            case ResultType.Draw: DrawCount++; break;
        }
    }

    private void SetRecordLabel()
    {
        if (null != RecordLabel)
        {
            RecordLabel.text = string.Format(Localization.Get("3"), WinCount, LoseCount, DrawCount);
        }
    }

    private void SetActiveVsObj(bool active)
    {
        if (null != VsObj)
        {
            VsObj.SetActive(active);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause == true)
        {
            // 앱이 백그라운드로 나갔을때
            Time.timeScale = 0;
        }
        else
        {
            // 앱이 포그라운드로 들어 왔을때
            Time.timeScale = 1;
        }
    }
}
