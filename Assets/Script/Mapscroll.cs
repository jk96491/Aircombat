using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapscroll : MonoBehaviour {

    [SerializeField]
    private float scrollSpeed = 0f;
    [SerializeField]
    private Vector3 terminalPos = Vector3.zero;
    [SerializeField]
    private Transform myTrans = null;
    [SerializeField]
    private Vector3 startPos = Vector3.zero;

    public bool isGameStart = false;

	void Update () {

        if (false == isGameStart)
            return;

        if (null != myTrans)
        {
            myTrans.Translate(new Vector3(0, 0, 1) * scrollSpeed * Time.deltaTime);

            if(myTrans.localPosition.z >= terminalPos.z)
            {
                myTrans.localPosition = startPos;
            }
        }
	}
}
