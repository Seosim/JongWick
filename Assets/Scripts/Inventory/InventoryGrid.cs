using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryGrid : MonoBehaviour, IPointerClickHandler
{
    public int Width;
    public int Height;
    public int Size;

    [SerializeField] private GameObject m_ItemPrefab;

    private InventoryItem[,] mInventoryItem;

    private RectTransform mRectTransform;
    private int2 mGridIndex;
    private GameObject mSelectedItem;

    private void Awake()
    {
        mRectTransform = GetComponent<RectTransform>();
        mInventoryItem = new InventoryItem[Width, Height];
    }

    private void Start()
    {
        mRectTransform.sizeDelta = new Vector2(Width * Size, Height * Size);

        //GameManager.Instance.player.GetComponent<PlayerInputController>().aLeftMouseDown += GetGridIndex;

        //InventoryItem item = Instantiate(m_ItemPrefab).GetComponent<InventoryItem>();
        //if(!PlaceItem(item, 5, 0))
        //    Destroy(item);

        //item = Instantiate(m_ItemPrefab).GetComponent<InventoryItem>();
        //if (!PlaceItem(item, 7, 0))
        //    Destroy(item);
    }

    private void Update()
    {
        if(mSelectedItem != null)
        {
            RectTransform rectTransform = mSelectedItem.GetComponent<RectTransform>();
            ItemData itemData = mSelectedItem.GetComponent<InventoryItem>().Data;
            Vector2 mousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

            Vector2 pivot = new Vector2(Size * itemData.Width / 4, -Size * itemData.Height / 4);

            rectTransform.anchoredPosition = mousePosition + pivot;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GetGridIndex();
        if (Mathf.Clamp(mGridIndex.x, 0, Width - 1) == mGridIndex.x && Mathf.Clamp(mGridIndex.y, 0, Height - 1) == mGridIndex.y)
        {
            if (mSelectedItem != null)
            {
                bool bEmpty = true;
                InventoryItem inventoryItem = mSelectedItem.GetComponent<InventoryItem>();

                foreach (var item in mInventoryItem)
                {
                    if (item == null)
                        continue;

                    if (!AABB(mGridIndex, new int2(inventoryItem.Data.Width, inventoryItem.Data.Height), item.pIndex, new int2(item.Data.Width, item.Data.Height)))
                    {
                        bEmpty = false;
                        break;
                    }
                }

                if (bEmpty)
                {
                    RectTransform rectTransform = mSelectedItem.GetComponent<RectTransform>();
                    rectTransform.SetParent(mRectTransform);

                    mInventoryItem[mGridIndex.x, mGridIndex.y] = inventoryItem;
                    inventoryItem.UpdateIndex(mGridIndex);
                    Vector2 position = new Vector2(mGridIndex.x * Size + Size * inventoryItem.Data.Width / 2.0f, -(mGridIndex.y * Size + Size * inventoryItem.Data.Height / 2.0f));

                    rectTransform.localPosition = position;

                    mSelectedItem = null;
                }
            }
            else
            {
                foreach(var item in mInventoryItem)
                {
                    if(item == null)
                        continue;   

                    if(!AABB(mGridIndex, new int2(1,1), item.pIndex, new int2(item.Data.Width, item.Data.Height)))
                    {
                        mSelectedItem = item.gameObject;
                        mInventoryItem[mGridIndex.x, mGridIndex.y] = null;
                        RectTransform rectTransform = mSelectedItem.GetComponent<RectTransform>();
                        rectTransform.SetParent(mRectTransform.parent);
                        break;
                    }
                }
            }
        }


    }

    public void SetSelectedItem(GameObject item)
    {
        if(mSelectedItem == null)
        {
            mSelectedItem = item;
            RectTransform rectTransform = mSelectedItem.GetComponent<RectTransform>();
            rectTransform.SetParent(mRectTransform.parent);
        }

    }

    public bool PlaceItem(DropItem dropItem)
    {
        int2 tileIndex = FindEmptyTile(dropItem.Data);

        if (tileIndex.x == -1)
            return false;

        InventoryItem item = Instantiate(m_ItemPrefab).GetComponent<InventoryItem>();
        item.Data = dropItem.Data;

        RectTransform rectTransform = item.GetComponent<RectTransform>();
        rectTransform.SetParent(mRectTransform);
        mInventoryItem[tileIndex.x, tileIndex.y] = item;
        rectTransform.sizeDelta = new Vector2(Mathf.Pow(item.Data.Width, 1.1f), Mathf.Pow(item.Data.Height, 1.1f));

        Vector2 position = new Vector2(tileIndex.x * Size + Size * item.Data.Width / 2.0f, -(tileIndex.y * Size + Size * item.Data.Height / 2.0f));
        rectTransform.localPosition = position;

        item.gameObject.GetComponent<Image>().sprite = item.Data.ItemSprite;

        item.UpdateData(item.Data);
        item.UpdateIndex(tileIndex);
        return true;
    }

    private void GetGridIndex()
    {
        Vector2 mousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

        Vector2 position = mousePosition - mRectTransform.anchoredPosition;

        mGridIndex.x = (int)(position.x / Size);
        mGridIndex.y = (int)(-position.y / Size);

    }

    private int2 FindEmptyTile(ItemData data)
    {
        List<InventoryItem> items = new List<InventoryItem>();

        foreach (InventoryItem invenItem in mInventoryItem)
        {
            if(invenItem != null)
            {
                items.Add(invenItem); 
            }
        }

        if (items.Count == 0)
            return new int2(0, 0);


        int2 index = int2.zero;
        int2 size = new int2(data.Width, data.Height);
        //x,y: Grid's Data, i,j: DropItem's Data
        for (int y = 0; y < Height - data.Height + 1; ++y)
        {
            for (int x = 0; x < Width - data.Width + 1; ++x)
            {
                index.x = x;
                index.y = y;

                bool bEmpty = true;

                foreach (InventoryItem invenItem in items)
                {
                    if (invenItem == null)
                        continue;

                    int2 invenItemSize = new(invenItem.Data.Width, invenItem.Data.Height);
                    if (!AABB(index, size, invenItem.pIndex, invenItemSize))
                    {
                        bEmpty = false;
                        break;
                    }
                }

                if(bEmpty)
                    return index;
            }
        }


        return new int2(-1, -1);
    }


    private bool AABB(int2 pos1, int2 size1, int2 pos2, int2 size2)
    {
        bool x1 = Mathf.Clamp(pos1.x, pos2.x, pos2.x + size2.x - 1) == pos1.x;
        bool y1 = Mathf.Clamp(pos1.y, pos2.y, pos2.y + size2.y - 1) == pos1.y;
        bool x2 = Mathf.Clamp(pos1.x + size1.x - 1, pos2.x, pos2.x + size2.x - 1) == pos1.x + size1.x - 1;
        bool y2 = Mathf.Clamp(pos1.y + size1.y - 1, pos2.y, pos2.y + size2.y - 1) == pos1.y + size1.y - 1;

        if (x1 && y1)
            return false;
        if (x1 && y2)
            return false;
        if (x2 && y1)
            return false;
        if (x2 && y2)
            return false;

        return true;
    }
}
