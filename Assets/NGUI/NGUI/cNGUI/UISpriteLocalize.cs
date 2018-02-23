using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpriteLocalize : MonoBehaviour
{
    [System.Serializable]
    public class LocalizeInfo
    {
        public string spriteName = string.Empty;
        public Vector2 widgetSize = Vector2.zero;
        [Header("transform변화시 체크필수")]
        public bool changeTransform = false;
        public Vector3 localPosition = Vector3.zero;
        public Vector3 localRotation = Vector3.zero;
    }

    private UISprite sprite;

    public List<LocalizeInfo> m_listInfo;

    private void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }
    private void OnEnable()
    {
        LocalizeInfo info = null;

      
        sprite.spriteName = info.spriteName;
        sprite.width = (int)info.widgetSize.x;
        sprite.height = (int)info.widgetSize.y;
        if (info.changeTransform)
        {
            this.transform.localPosition = info.localPosition;
            this.transform.eulerAngles = info.localRotation;
        }
    }

}
