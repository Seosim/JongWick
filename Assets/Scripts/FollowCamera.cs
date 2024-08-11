using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject mPivot;

    private void Start()
    {
        mPivot = GameManager.gameManager.player;
    }
    void LateUpdate()
    {
        transform.position = mPivot.transform.position + new Vector3(0, 0, -10);
    }
}
