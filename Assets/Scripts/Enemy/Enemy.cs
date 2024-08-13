using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float DetectingRange = 0.0f;

    public float MoveDelay = 1.0f;
    public float Speed = 5.0f;

    private Rigidbody2D mRigidBody;
    private BTEnemy mBTEnemy;
    private float mMoveDelay = 0.0f;
    private Vector3 mDirection = Vector2.zero;

    private bool mWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mBTEnemy = new BTEnemy(initializeBT());

        mMoveDelay = MoveDelay;
    }

    private void FixedUpdate()
    {

        if(mWalk)
            mRigidBody.MovePosition(transform.position + mDirection * Speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        mBTEnemy.Operate();
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

    private INode.State FindPlayer()
    {
        Vector2 playerPosition = GameManager.gameManager.player.transform.position;
        float distance = Vector2.Distance(playerPosition, transform.position);

        if(distance < DetectingRange)
        {
            return INode.State.Success;
        }

        return INode.State.Failure;
    }

    private INode.State AttackPlayer()
    {
        print("Attack AI");
        mWalk = false;
        return INode.State.Success;
    }

    private INode.State SetMoveDirection()
    {
        if(mMoveDelay == MoveDelay)
        {
            mDirection = Random.insideUnitCircle.normalized;
            mWalk = true;
        }
        return INode.State.Success;
    }

    private INode.State WalkAround()
    {
        if(mMoveDelay <= 0.0f)
        {
            mMoveDelay = MoveDelay;
            return INode.State.Success;
        }
        else
        {
            mMoveDelay -= Time.deltaTime;
            return INode.State.Running;
        }
    }
}
