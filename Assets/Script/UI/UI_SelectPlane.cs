using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectPlane : UI_LayerBase {

    [SerializeField]
    private Button exitBtn = null;
    [SerializeField]
    private List<UI_PlaneView> planeViewList = new List<UI_PlaneView>();

    private GameObject Plane = null;

    private List<PlaneRecord.PlaneInfo> planeList = new List<PlaneRecord.PlaneInfo>();

    protected override void Initailize()
    {
        if (null != exitBtn)
        {
            exitBtn.onClick.AddListener(HandleOnClickExit);
        }

        Refresh();
    }

    private void SetData()
    {
        planeList.Clear();

        var PlaneEtor = GameData.Instance.planeRecord.GetPlaneEtor();

        while (true == PlaneEtor.MoveNext())
        {
            planeList.Add(PlaneEtor.Current.Value);
        }
        for (int i = 0; i < planeViewList.Count; i++)
        {
            planeViewList[i].dataIndex = planeList[i].ID;
            planeViewList[i].onClick = HandleOnClickPlane;

            string path = GameData.Instance.resourceRecord.FindResourcePathByID(planeList[i].TextureID);

            Sprite texture = Resources.Load(path, typeof(Sprite)) as Sprite;
            planeViewList[i].SetImage(texture);
        }
    }

    public void HandleOnClickPlane(UI_ViewBase viewBase_)
    {
        PlaneRecord.PlaneInfo info = GameData.Instance.planeRecord.FindPlane(viewBase_.dataIndex);

        string path = GameData.Instance.resourceRecord.FindResourcePathByID(info.ModelID);

        GameObject planePrefab = Resources.Load(path) as GameObject;

        if(null != planePrefab)
        {
            if(null != Plane)
            {
                Destroy(Plane);
            }
            Plane = Instantiate(planePrefab);
            Plane.transform.localPosition = new Vector3(0, 0 ,10);
        }
    }

    private void HandleOnClickExit()
    {
        UIManager.Instance.OpenUI(UIManager.UIType.UI_LOBBY);
        UIManager.Instance.CloseUI(UIManager.UIType.UI_SELECT_PLANE);
    }

    protected override void Refresh()
    {
        SetData();
    }

    protected override void DeActivate()
    {
        if (null != Plane)
        {
            Destroy(Plane);
        }

        planeViewList.Clear();
        planeList.Clear();
    }
}
