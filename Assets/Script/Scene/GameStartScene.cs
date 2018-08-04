using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScene : MonoBehaviour {

    [SerializeField]
    private UILabel titleLabel = null;
    [SerializeField]
    private UILabel startLabel = null;

    private LocalzationManager LocalizationManagerIns = null;

    // Use this for initialization
    void Start () {
        LocalizationManagerIns = LocalzationManager.Instant;
        LocalizationManagerIns.Init( LocalzationManager.LanType.KOREAN);

        if (null != titleLabel)
            titleLabel.text = Localization.Get("1");
        if (null != startLabel)
            startLabel.text = Localization.Get("2");
    }

    public void HandleOnClickLanTemp()
    {
        /*
        if(LocalizationManagerIns.currentLan == LocalzationManager.LanType.KOREAN)
            LocalizationManagerIns.Init(LocalzationManager.LanType.ENGLISH);
        else if (LocalizationManagerIns.currentLan == LocalzationManager.LanType.ENGLISH)
            LocalizationManagerIns.Init(LocalzationManager.LanType.KOREAN);

        if (null != titleLabel)
            titleLabel.text = Localization.Get("1");
        if (null != startLabel)
            startLabel.text = Localization.Get("2");
           */

        SceneManager.LoadScene("GameScene");

    }
}
