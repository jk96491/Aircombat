using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickEffector : MonoBehaviour {

    private void Awake()
    {
        UIEventListener.Get(gameObject).onPress = HandleOnPress;
    }

    private void HandleOnPress(GameObject obj, bool press_)
    {
        TweenScale tweenScale = gameObject.GetComponent<TweenScale>();
        
        if(null == tweenScale)
        {
            tweenScale = gameObject.AddComponent<TweenScale>();
        }

        tweenScale.duration = 0.05f;

        if (true == press_)
        {
            tweenScale.from = Vector3.one;
            tweenScale.to = Vector3.one * 0.9f;
        }
        else
        {
            tweenScale.from = Vector3.one * 0.9f;
            tweenScale.to = Vector3.one;
        }

        tweenScale.PlayForward();
    }
}
