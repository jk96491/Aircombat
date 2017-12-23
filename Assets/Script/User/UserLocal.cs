using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserLocal : UserBase
{
    public class UserMoney
    {
        public int gold;
        public int gem;
    }

    public UserLocal()
    {
        Init();
    }

    private int _currentPlaneID; //  임시코드

    public int CurrentPlane
    {
        get
        {
            string planeKey = string.Format("{0}:myPlane", userID);
            _currentPlaneID = PlayerPrefs.GetInt(planeKey, -1);
            if (_currentPlaneID == -1)
            {
                _currentPlaneID = 10101;
            }

            return _currentPlaneID;
        }
    }

    public void SetPlane(int planeID_)
    {
        _currentPlaneID = planeID_;
    }


    public UserMoney money = new UserMoney();

    private Dictionary<int/*currentPlaneID*/, PlaneBase> planeDic_ = new Dictionary<int, PlaneBase>();

    public void Init()
    {
        string planeKey = string.Format("{0}:myPlane", userID);
        _currentPlaneID = PlayerPrefs.GetInt(planeKey, -1);

        if(_currentPlaneID == -1)
        {
            _currentPlaneID = 10101;
        }
    } 

    public string GetCurrentPlane()
    {
        PlaneRecord.PlaneInfo info = GameData.Instance.planeRecord.FindPlane(_currentPlaneID);

        string path = GameData.Instance.resourceRecord.FindResourcePathByID(info.ModelID);
        return path;
    }

    public void SetMoney(UserMoney money_)
    {
        money.gold = money_.gold;
        money.gem = money_.gem;
    }
}
