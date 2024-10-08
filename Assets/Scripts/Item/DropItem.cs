using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class DropItem : MonoBehaviour
{
    public ItemData Data;

    public Sprite pSprite { get { return mSpriteRenderer.sprite; } set { mSpriteRenderer.sprite = value; } }


    private SpriteRenderer mSpriteRenderer;
    private bool mbActive = false;

    public void Init(ItemData data)
    {
        Data = data;
        mSpriteRenderer.sprite = data.ItemSprite;
    }

    private void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerInputController inputController = GameManager.Instance.player.GetComponent<PlayerInputController>();
        inputController.aGKeyDown += GetItem;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mbActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mbActive = false;
        }
    }

    private void GetItem()
    {
        if (mbActive)
        {
            GameManager.Instance.inventory.PlaceItem(this);
            //TODO: 인벤토리에 아이템이 들어가게 코드 수정
            //bool bAdd = !GameManager.Instance.inventory.AddItem(this);
            //gameObject.SetActive(bAdd);
        }
    }
}
