using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalzationManager : MonoBehaviour {

    public enum LanType : sbyte
    {
        NONE = -1,
        KOREAN,
        ENGLISH,
    }

    public LanType currentLan;


    static LocalzationManager _Instant = null;

	//---------------------------------------------------
	// Attribute Funtion.

	public static LocalzationManager Instant
	{
		get
		{
			if (_Instant == null)
			{
				_Instant = new LocalzationManager();
			}

			return _Instant;
		}
	}

    public void Init(LanType lanType_)
    {
        currentLan = lanType_;
        Object localizeAsset = null;

        switch (lanType_)
        {
            case LanType.KOREAN: localizeAsset = Resources.Load("Table/Korean"); break;
            case LanType.ENGLISH: localizeAsset = Resources.Load("Table/English"); break;
        }

        
        if (localizeAsset != null)
            Localization.Load((TextAsset)localizeAsset);

    }

    public void ChangeLan(LanType lanType_)
    {
        Init(lanType_);
    }

}
