using UnityEngine;
using System.Collections;


public class cUISprite : UISprite
{
    public enum AtlasType
    {
        Atlas1 = 0,
        Atlas2,
    }

    public AtlasType m_atlasType = AtlasType.Atlas1;
    public string m_spriteName = "";

	protected override void OnEnable()
	{
        base.OnEnable();

        if (atlas != null)
            return;
        //atlas = LoadDataManager.Instance.GetAtlas(m_atlasKind);
        spriteName = m_spriteName;
	}
    void OnDestroy()
    {
        atlas = null;
    }
}
