﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.Instance.OpenUI(UIManager.UIType.UI_LOBBY);
	}
}
