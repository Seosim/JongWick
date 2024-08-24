using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAI : MonoBehaviour
{
    [SerializeField] private GameObject m_Bullet;
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

    private Enemy mEnemy;

    private void Start()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mEnemy = GetComponentInParent<Enemy>();
        mEnemy.aAttack += Shoot;

        mAmmo = new List<GameObject>();
        //TODO: 총알 처리 필요
        mRemainAmmoCount = 15;
        for (int i = 0; i < 15; i++)
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
            if(mEnemy.bAttack)
            {
                Vector2 playerPosition = GameManager.Instance.player.transform.position;
                mDirection = new Vector2(transform.position.x - playerPosition.x, transform.position.y - playerPosition.y);

                float angle = Mathf.Atan2(mDirection.x, -mDirection.y) * Mathf.Rad2Deg;
                mSpriteRenderer.flipY = angle > 0.0f;
                angle += 90.0f;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
            }

        }

        //Recoil Recovery
        {
            mRecoil = Mathf.Lerp(mRecoil, 0.0f, Time.deltaTime * RecoilRecovery);
        }
        //Fire Rate
        {
            if (mFireRate > 0.0f)
            {
                mFireRate = Mathf.Max(mFireRate - Time.deltaTime, 0.0f);
            }
        }
    }

    private void Shoot()
    {
        if (mFireRate > 0.0f)
            return;

        foreach (GameObject obj in mAmmo)
        {
            if (!obj.activeSelf)
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
    }
}
