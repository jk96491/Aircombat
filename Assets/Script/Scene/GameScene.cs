using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
    [SerializeField]
    private Mapscroll map;
    [SerializeField]
    private Mapscroll map2;


	// Use this for initialization
	void Start () {
        map.isGameStart = false;
        map2.isGameStart = false;
        StartCoroutine(Delay());
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.OpenUI(UIManager.UIType.UI_GAME);
        map.isGameStart = true;
        map2.isGameStart = true;
    }
	
}
