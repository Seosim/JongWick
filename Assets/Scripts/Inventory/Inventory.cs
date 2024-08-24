using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public int2 Size;

    [SerializeField] private GameObject m_InventoryTile;


    private InvenItem[,] mInventoryTiles;

    public bool AddItem(DropItem dropItem)
    {
        int2 itemSize = new int2(1, 1);
        switch(dropItem.pSize)
        {
            case InvenItem.eSize.OneXOne:
                itemSize = new int2(1, 1);
                break;
            case InvenItem.eSize.TwoXTwo:
                itemSize = new int2(2, 2);
                break;
        }

        for (int i = 0; i <= Size.y - itemSize.y; i++)
        {
            for (int j = 0; j <= Size.x - itemSize.x; j++)
            {
                bool bEmpty = true;

                for(int y = 0; y < itemSize.y; ++y)
                {

                    for(int x = 0; x < itemSize.x; ++x)
                    {
                        if (mInventoryTiles[j + x, i + y].Size != InvenItem.eSize.None)
                        {
                            bEmpty = false;
                            break;
                        }
                    }

                    if (!bEmpty)
                        break;
                }

                if (bEmpty)
                {
                    for (int y = 0; y < itemSize.y; ++y)
                    {
                        for (int x = 0; x < itemSize.x; ++x)
                        {
                            print($"{j + x}, {i + y}");

                            mInventoryTiles[j + x, i + y].pSprite = dropItem.pSprite;
                            mInventoryTiles[j + x, i + y].Size = dropItem.pSize;
                        }
                    }
                    return bEmpty;
                }
            }
        }
        return false;
    }

    private void Awake()
    {
        mInventoryTiles = new InvenItem[Size.x, Size.y];

        for (int i = 0; i < Size.y; i++)
        {
            for (int j = 0; j < Size.x; j++)
            {
                GameObject inventory = Instantiate(m_InventoryTile, transform);
                mInventoryTiles[j, i] = inventory.GetComponent<InvenItem>();
            }
        }
    }
}
