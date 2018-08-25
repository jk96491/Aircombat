using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScene : MonoBehaviour {

    [SerializeField]
    private UILabel titleLabel = null;
    [SerializeField]
    private UILabel startLabel = null;
    [SerializeField]
    private UILabel languageSelectBtnLabel = null;

    private LocalzationManager LocalizationManagerIns = null;

    // Use this for initialization
    void Start () {
        LocalizationManagerIns = LocalzationManager.Instant;
        LocalizationManagerIns.Init( LocalzationManager.LanType.KOREAN);

        SetLanguageSelectBtnLabel();

        if (null != titleLabel)
            titleLabel.text = Localization.Get("1");
        if (null != startLabel)
            startLabel.text = Localization.Get("2");

        if(null != startLabel)
        {
            UIEventListener.Get(startLabel.gameObject).onClick = HandleOnClickStartLabel; 
        }
    }

    private void SetLanguageSelectBtnLabel()
    {

        if (null == languageSelectBtnLabel)
            return;

        switch (LocalizationManagerIns.currentLan)
        {
            case LocalzationManager.LanType.KOREAN:
                {
                    languageSelectBtnLabel.text = "Eng";
                }
                break;
            case LocalzationManager.LanType.ENGLISH:
                {
                    languageSelectBtnLabel.text = "한글";
                }
                break;
        }
    }

    public void HandleOnClickLanTemp()
    {
        
        if(LocalizationManagerIns.currentLan == LocalzationManager.LanType.KOREAN)
            LocalizationManagerIns.Init(LocalzationManager.LanType.ENGLISH);
        else if (LocalizationManagerIns.currentLan == LocalzationManager.LanType.ENGLISH)
            LocalizationManagerIns.Init(LocalzationManager.LanType.KOREAN);

        if (null != titleLabel)
            titleLabel.text = Localization.Get("1");
        if (null != startLabel)
            startLabel.text = Localization.Get("2");

        SetLanguageSelectBtnLabel();

    }

    private void HandleOnClickStartLabel(GameObject obj)
    {
        SceneManager.LoadScene("GameScene");
    }
}
