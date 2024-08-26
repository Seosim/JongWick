using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    public Sprite pSprite { get { return mSpriteRenderer.sprite; } set { mSpriteRenderer.sprite = value; } }

    private SpriteRenderer mSpriteRenderer;

    private void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.Translate(0, 0, 10.0f);
    }
}
