using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
    [SerializeField]
    private UIToggleController SelectController = null;

	// Use this for initialization
	void Start ()
    {
        if(SelectController != null)
        {
            SelectController.InitToggles();
            if (null == SelectController.clickToggleDel)
                SelectController.clickToggleDel = HandleOnSelect;
        }
	}

    public void HandleOnSelect(int clickIndex)
    {
        Debug.LogError(clickIndex);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
