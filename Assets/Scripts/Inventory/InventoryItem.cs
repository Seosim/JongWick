using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public int2 pIndex { get { return mIndex; } private set {  mIndex = value; } }
    public int2 pSize { get {
            if (!mbRotate)
                return new int2(Data.Width, Data.Height);
            else
                return new int2(Data.Height, Data.Width);
                }
    }

    public ItemData Data;
    public bool mbRotate = false;

    private int2 mIndex;

    public void UpdateData(ItemData data)
    {
        Data = data;
    }

    public void UpdateIndex(int2 index)
    {
        mIndex = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.inventory.SetSelectedItem(gameObject);
    }
}
