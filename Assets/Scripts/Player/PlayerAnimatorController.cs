using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    private PlayerInputController mPlayerInputController;
   

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GameManager.Instance.player.GetComponent<SpriteRenderer>();
        mPlayerInputController = GameManager.Instance.player.GetComponent<PlayerInputController>();

        Debug.Assert(mAnimator != null);
        Debug.Assert(mSpriteRenderer != null);
        Debug.Assert(mPlayerInputController != null);
    }

    private void Update()
    {
        if(mPlayerInputController.Direction.x != 0)
        {
            mAnimator.SetBool("Input", true);
            mSpriteRenderer.flipX = mPlayerInputController.Direction.x < 0;
        }
        else if (mPlayerInputController.Direction.y != 0)
        {
            mAnimator.SetBool("Input", true);
        }
        else
        {
            mAnimator.SetBool("Input", false);
        }
    }
}
