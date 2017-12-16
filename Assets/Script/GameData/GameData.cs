using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public TableRecord tableRecord = null;
    public ResourceRecord resourceRecord = null;
    public PlaneRecord planeRecord = null;

    public enum DataType
    {
        NONE = -1,
        TABLE,
        RESOURCE,
        PLANE
    }

    static private GameData instance = new GameData();
    static public GameData Instance { get { return instance; } }

    private string tablePath = "Table/Table";
    public const int INVALID_ID = -1;

    private Dictionary<DataType, IRecord> _recordDic = new Dictionary<DataType, IRecord>();

    public void LoadTableData()
    {
        if(null == this.tableRecord)
        {
            this.tableRecord = new TableRecord();
        }

        TextAsset textAsset = Resources.Load(this.tablePath) as TextAsset;

        if(null == textAsset)
        {
            Debug.LogError("TextAsset is null");
            return;
        }

        JSONNode root = JSON.Parse(textAsset.text);
        this.tableRecord.DeSerialize(root);
        LoadAllData();
    }

    private void LoadAllData()
    {
        TableRecord.TableInfo[] tableInfo = this.tableRecord.info;

        if(null != tableInfo)
        {
            for(int index = 0; index < tableInfo.Length; index++)
            {
                TextAsset textAsset = Resources.Load(tableInfo[index].path) as TextAsset;

                if (null == textAsset)
                {
                    Debug.LogError("TextAsset is null");
                    return;
                }

                JSONNode root = JSON.Parse(textAsset.text);

                if(true == this._recordDic.ContainsKey(tableInfo[index].type))
                {
                    GetRecord(tableInfo[index].type).DeSerialize(root);
                }
                else
                {
                    IRecord record = GetTypeRecord(tableInfo[index].type);
                    record.DeSerialize(root);
                    InsertData(record, tableInfo[index].type);
                    this._recordDic.Add(tableInfo[index].type, record);
                }
            }
        }
    }

    private IRecord GetTypeRecord(DataType type_)
    {
        switch (type_)
        {
            case DataType.TABLE: { return new TableRecord(); }
            case DataType.RESOURCE: { return new ResourceRecord(); }
            case DataType.PLANE: { return new PlaneRecord(); }
        }
        return null;
    }

    private void InsertData(IRecord record_, DataType type_)
    {
        switch (type_)
        {
            case DataType.TABLE: { this.tableRecord = record_ as TableRecord; } break;
            case DataType.RESOURCE: { this.resourceRecord = record_ as ResourceRecord; } break;
            case DataType.PLANE: { this.planeRecord = record_ as PlaneRecord; } break;
        }

    }

    private IRecord GetRecord(DataType type_)
    {
        switch (type_)
        {
            case DataType.TABLE: return this.tableRecord;
            case DataType.PLANE: return this.planeRecord;
            case DataType.RESOURCE: return this.resourceRecord;
        }
        return null;
    }
}
