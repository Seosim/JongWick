using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Action aShoot;

    public float pAngle { get { return mAngle; } }

    [SerializeField] private GameObject m_Bullet;
    public int MaxAmmo = 12;
    public float ReloadTime = 3.0f;
    public float MaxRecoil = 0.1f;
    public float Recoil = 0.0f;
    public float RecoilRecovery = 1.0f;
    public float FireRate = 1.0f;

    private SpriteRenderer mSpriteRenderer;
    private Vector2 mDirection;
    private List<GameObject> mAmmo;
    private int mRemainAmmoCount;
    
    private float mReloadTime = 0.0f;
    private float mRecoil = 0.0f;
    private float mFireRate = 0.0f;

    private bool mbAttack = false;
    private float mAngle = 0.0f;

    private void Start()
    {
        GameManager.Instance.player.GetComponent<PlayerInputController>().aLeftMouseDown += StartAttack;
        GameManager.Instance.player.GetComponent<PlayerInputController>().aLeftMouseUp += StopAttack;
        GameManager.Instance.player.GetComponent<PlayerInputController>().aRKeyDown += Reload;

        mSpriteRenderer = GetComponent<SpriteRenderer>();

        mAmmo = new List<GameObject>();
        mRemainAmmoCount = MaxAmmo;
        for (int i = 0; i < MaxAmmo; i++)
        {
            GameObject obj = Instantiate(m_Bullet);
            obj.SetActive(false);
            mAmmo.Add(obj);
        }
    }

    void Update()
    {
        //Rotate Weapon
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            mDirection = new Vector2(screenPoint.x - Input.mousePosition.x, screenPoint.y - Input.mousePosition.y);
            float angle = Mathf.Atan2(mDirection.x, -mDirection.y) * Mathf.Rad2Deg;
            mAngle = angle;
            mSpriteRenderer.flipY = angle > 0.0f;
            angle += 90.0f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }

        //Attack
        {
            if(mbAttack)
            {
                Shoot();
            }
        }

        //Reload Timer
        {
            if(mReloadTime > 0.0f)
            {
                mReloadTime -= Time.deltaTime;

                if(mReloadTime < 0.0f)
                {
                    mRemainAmmoCount = MaxAmmo;
                    mReloadTime = 0.0f;
                }
            }
        }

        //Recoil Recovery
        {
            mRecoil = Mathf.Lerp(mRecoil, 0.0f, Time.deltaTime * RecoilRecovery);
        }
        //Fire Rate
        {
            if(mFireRate > 0.0f)
            {
                mFireRate = Mathf.Max(mFireRate - Time.deltaTime, 0.0f);
            }
        }
    }

    private void StartAttack()
    {
        mbAttack = true;
    }

    private void StopAttack()
    {
        mbAttack = false;
    }

    private void Shoot()
    {
        if (mRemainAmmoCount == 0 || mReloadTime != 0.0f || mFireRate > 0.0f)
            return;

        foreach(GameObject obj in mAmmo)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                Bullet bullet = obj.GetComponent<Bullet>();

                Vector2 direction = -mDirection.normalized + new Vector2(UnityEngine.Random.Range(-mRecoil, mRecoil), UnityEngine.Random.Range(-mRecoil, mRecoil));
                bullet.Initialize(transform.position + transform.right, direction.normalized, transform.rotation);
                --mRemainAmmoCount;
                mRecoil = Mathf.Min(MaxRecoil, mRecoil + Recoil);
                mFireRate = FireRate;
                break;
            }
        }

        aShoot?.Invoke();
    }

    private void Reload()
    {
        mReloadTime = ReloadTime;
    }
}
