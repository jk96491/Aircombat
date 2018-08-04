using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalzationManager : MonoBehaviour {

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

    public void Initi()
    {
        Object localizeAsset = null;
        localizeAsset = Resources.Load("Table/Korean");
        if (localizeAsset != null)
            Localization.Load((TextAsset)localizeAsset);

    }

}
