using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRecord : IRecord {
    public class PlaneInfo
    {
        public int ID = GameData.INVALID_ID;
        public int TextureID = GameData.INVALID_ID;
        public int ModelID = GameData.INVALID_ID;
        public string Name = string.Empty;
        public string Desc = string.Empty;
    }

    public PlaneInfo[] planeInfo = null;
    private Dictionary<int/*ID*/, PlaneInfo> _dataDic = new Dictionary<int, PlaneInfo>();


    public override void DeSerialize(JSONNode root)
    {
        var rootInfo = root["Info"];
        this.planeInfo = new PlaneInfo[rootInfo.Count];

        for (int index = 0; index < this.planeInfo.Length; index++)
        {
            if (null == this.planeInfo[index])
                this.planeInfo[index] = new PlaneInfo();
            planeInfo[index].ID = rootInfo[index]["ID"];
            planeInfo[index].TextureID = rootInfo[index]["Texture"];
            planeInfo[index].ModelID = rootInfo[index]["ModelID"];
            planeInfo[index].Name = rootInfo[index]["Name"];
            planeInfo[index].Desc = rootInfo[index]["Desc"];
        }
        DeSerialize();
    }

    public void DeSerialize()
    {
        for (int index = 0; index < this.planeInfo.Length; index++)
        {
            if (null != this.planeInfo[index])
            {
                if (null != this.planeInfo[index])
                {
                    if (false == this._dataDic.ContainsKey(this.planeInfo[index].ID))
                        this._dataDic[this.planeInfo[index].ID] = this.planeInfo[index];
                }
            }
        }
    }

    public Dictionary<int/*ID*/, PlaneInfo>.Enumerator GetPlaneEtor()
    {
        return this._dataDic.GetEnumerator();
    }
}
