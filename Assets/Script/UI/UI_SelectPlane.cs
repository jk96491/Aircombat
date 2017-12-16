using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectPlane : UI_LayerBase {

    [SerializeField]
    private Button exitBtn = null;
    [SerializeField]
    private Image[] planeImage = null;

    private List<PlaneRecord.PlaneInfo> planeList = new List<PlaneRecord.PlaneInfo>();

    protected override void Initailize()
    {
        if (null != exitBtn)
        {
            exitBtn.onClick.AddListener(HandleOnClickExit);
        }

        planeList.Clear();

        var PlaneEtor = GameData.Instance.planeRecord.GetPlaneEtor();

        while(true == PlaneEtor.MoveNext())
        {
            planeList.Add(PlaneEtor.Current.Value);
        }

        for(int i = 0; i < planeList.Count;i++)
        {
            string path = GameData.Instance.resourceRecord.FindResourcePathByID(planeList[i].TextureID);
            
            Sprite texture = Resources.Load(path, typeof(Sprite)) as Sprite;

            planeImage[i].sprite = texture;
        }

    }

    private void HandleOnClickExit()
    {
        UIManager.Instance.OpenUI(UIManager.UIType.UI_LOBBY);
        UIManager.Instance.CloseUI(UIManager.UIType.UI_SELECT_PLANE);
    }
}
