using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using System;

public class TableRecord : IRecord
{
    public class TableInfo
    {
        public GameData.DataType type = GameData.DataType.NONE;
        public string path = string.Empty;
    }

    public TableInfo[] info = null;

    private Dictionary<GameData.DataType, string /*paht*/> _dataDic = new Dictionary<GameData.DataType, string>();

    public override void DeSerialize(JSONNode root)
    {
        var rootInfo = root["Info"];
        info = new TableInfo[rootInfo.Count];

        for(int index = 0; index < info.Length; index++)
        {
            if (null == info[index])
                info[index] = new TableInfo();
            info[index].type = (GameData.DataType)Enum.Parse(typeof(GameData.DataType), rootInfo[index]["type"].Value);
            info[index].path = rootInfo[index]["path"].Value;
        }
    }

}
