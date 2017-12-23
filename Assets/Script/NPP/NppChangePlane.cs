using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NppChangePlane
{
    static string userID = string.Empty;
    static int planID = GameData.INVALID_ID;
    static Action<int> successCallback = null;
    static Action<int> failCallback = null;

    public static void RequestPlaneChange(string userID_, int planeID_, Action<int> successCallback_, Action<int> failCallback_)
    {
        userID = userID_;
        planID = planeID_;
        successCallback = successCallback_;
        failCallback = failCallback_;

        string planeKey = string.Format("{0}:myPlane", userID_);
        PlayerPrefs.SetInt(planeKey, planID);

        if(null != successCallback_)
            successCallback_(planID);
    }
	
}
