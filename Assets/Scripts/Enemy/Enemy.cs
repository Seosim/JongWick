using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Action aHit;
    public Action aAttack;

    public float DetectingRange = 0.0f;

    public float MoveDelay = 3.0f;
    public float MoveTime = 1.0f;
    public float Speed = 5.0f;
    public float MaxHp = 10.0f;

    public bool bAttack { get { return !mbWalk; } private set { } }

    private Rigidbody2D mRigidBody;
    private Animator mAnimator;
    private BTEnemy mBTEnemy;
    private SpriteRenderer mSpriteRenderer;
    private Vector3 mDirection = Vector2.zero;
    private float mMoveDelay = 0.0f;
    private float mMoveTime = 0.0f;
    private float mHp = 0.0f;

    private bool mbWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mBTEnemy = new BTEnemy(initializeBT());

        mMoveTime = MoveTime;
        mHp = MaxHp;
    }

    private void FixedUpdate()
    {
        if(mbWalk)
            mRigidBody.MovePosition(transform.position + mDirection * Speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        mBTEnemy.Operate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            mHp -= bullet.Damage;
            aHit?.Invoke();
        }
    }

    private INode initializeBT()
    {
        return new SelectorNode(
            new List<INode>()
            {
                new SequenceNode(
                    new List<INode>()
                    {
                        new ActionNode(FindPlayer),
                        new ActionNode(AttackPlayer)
                    }),

                new SequenceNode(
                    new List<INode>()
                    {
                        new ActionNode(SetMoveDirection),
                        new ActionNode(WalkAround)
                    })
            });
    }

#region Attack
    private INode.State FindPlayer()
    {
        Vector2 playerPosition = GameManager.Instance.player.transform.position;
        float distance = Vector2.Distance(playerPosition, transform.position);

        if(distance < DetectingRange)
        {
            return INode.State.Success;
        }

        return INode.State.Failure;
    }

    private INode.State AttackPlayer()
    {
        aAttack?.Invoke();
        mbWalk = false;
        mAnimator.SetBool("Move", mbWalk);
        return INode.State.Success;
    }
    #endregion

#region Move
    private INode.State SetMoveDirection()
    {
        if(mMoveDelay >= 0.0f)
        {
            mMoveDelay -= Time.deltaTime;
            mDirection = Vector2.zero;
            mbWalk = false;
            mAnimator.SetBool("Move", mbWalk);
            return INode.State.Running;
        }

        if (mMoveTime == MoveTime)
        {
            mDirection = UnityEngine.Random.insideUnitCircle.normalized;
            mbWalk = true;
            mSpriteRenderer.flipX = mDirection.x < 0;
            mAnimator.SetBool("Move", mbWalk);
        }
        return INode.State.Success;
    }

    private INode.State WalkAround()
    {
        if(mMoveTime <= 0.0f)
        {
            mMoveTime = MoveTime;
            mMoveDelay = MoveDelay;
            return INode.State.Success;
        }
        else
        {
            mMoveTime -= Time.deltaTime;
            return INode.State.Running;
        }
    }
    #endregion
}
