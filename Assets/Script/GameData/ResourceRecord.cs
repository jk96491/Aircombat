using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class ResourceRecord : IRecord {

    public const int BLACK_PLANE_TEXTURE = 101;
    public const int BLUE_PLANE_TEXTURE = 102;
    public const int RED_PLANE_TEXTURE = 103;
    public const int GREEN_PLANE_TEXTURE = 104;

    public class ResourceInfo
    {
        public int ID = GameData.INVALID_ID;
        public string path = string.Empty;
    }

    public ResourceInfo[] Info = null;
    private Dictionary<int/*ID*/, string/*path*/> _dataDic = new Dictionary<int, string>();

    public override void DeSerialize(JSONNode root)
    {
        var rootInfo = root["Info"];
        this.Info = new ResourceInfo[rootInfo.Count];

        for(int index = 0; index < this.Info.Length; index++)
        {
            if (null == this.Info[index])
                this.Info[index] = new ResourceInfo();
            Info[index].ID = rootInfo[index]["ID"];
            Info[index].path = rootInfo[index]["path"];
        }

        DeSerialize();
    }

    public void DeSerialize()
    {
        for(int index = 0; index < this.Info.Length; index++)
        {
            if(null != this.Info[index])
            {
                if(null != this.Info[index])
                {
                    if (false == this._dataDic.ContainsKey(this.Info[index].ID))
                        this._dataDic[this.Info[index].ID] = this.Info[index].path; 
                }
            }
        }
    }

    public string FindResourcePathByID(int ID_)
    {
        string path = null;

        if (true == this._dataDic.ContainsKey(ID_))
            path = this._dataDic[ID_];

        return path;
    }

}
