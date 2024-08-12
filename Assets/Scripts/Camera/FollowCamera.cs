using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector2 Weight;
    public float RecoilTime;

    [SerializeField] private Weapon m_Weapon;

    private GameObject mPivot;
    private Vector2 mShake;

    private void Start()
    {
        mPivot = GameManager.gameManager.player;
        m_Weapon.aShoot += ShakeCamera;
    }
    void LateUpdate()
    {
        transform.position = mPivot.transform.position + new Vector3(mShake.x, mShake.y, -10);

        mShake = Vector2.Lerp(mShake, Vector2.zero, Time.deltaTime * RecoilTime);
    }


    private void ShakeCamera()
    {
        mShake = new Vector2(Random.Range(-Weight.x, Weight.x), Random.Range(-Weight.y, Weight.y));
    }
}
