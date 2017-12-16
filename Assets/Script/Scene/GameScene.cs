using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        StartCoroutine(Delay());
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.OpenUI(UIManager.UIType.UI_GAME);
    }
	
}
