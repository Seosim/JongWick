using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Action aShoot;

    [SerializeField] private GameObject m_Bullet;

    private SpriteRenderer mSpriteRenderer;

    private Vector2 mDirection;

    private void Start()
    {
        GameManager.gameManager.player.GetComponent<PlayerInputController>().aLeftMouseDown += Shoot;

        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        mDirection = new Vector2(screenPoint.x - Input.mousePosition.x, screenPoint.y - Input.mousePosition.y);
        float angle = Mathf.Atan2(mDirection.x, -mDirection.y) * Mathf.Rad2Deg;
        mSpriteRenderer.flipY = angle > 0.0f;
        angle += 90.0f;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    private void Shoot()
    {
        GameObject obj = Instantiate(m_Bullet);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.Initialize(transform.position + transform.right, -mDirection.normalized, transform.rotation);
        aShoot?.Invoke();
    }
}
