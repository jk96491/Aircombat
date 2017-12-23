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

    public int _currentPlaneID = 10101; //  임시코드

    public UserMoney money = new UserMoney();

    private Dictionary<int/*currentPlaneID*/, PlaneBase> planeDic_ = new Dictionary<int, PlaneBase>();

    public void Init()
    {
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
