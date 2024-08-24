using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour , IPointerClickHandler
{
    public enum eSize { 
        None,
        OneXOne,
        TwoXTwo
    };

    public Sprite pSprite { get { return mImage.sprite; } 
        set { mImage.sprite = value;
            mImage.SetNativeSize();
            mImage.color = mImage.sprite == null ? new UnityEngine.Color(0, 0, 0, 0) : new UnityEngine.Color(1, 1, 1, 1);
        } }

    public eSize Size = eSize.None;

    private Image mImage;

    static SelectItem mSelectItem;

    private void Awake()
    {
        if (mSelectItem == null)
            mSelectItem = GameObject.Find("SelectedItem").GetComponent<SelectItem>();

        mImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Sprite temp = mImage.sprite;
        eSize size = Size;

        mImage.sprite = mSelectItem.pSprite;
        Size = mSelectItem.Size;

        mSelectItem.pSprite = temp;
        mSelectItem.Size = size;
        mImage.SetNativeSize();

        mImage.color = mImage.sprite == null ? new UnityEngine.Color(0, 0, 0, 0) : new UnityEngine.Color(1, 1, 1, 1);
    }
}
