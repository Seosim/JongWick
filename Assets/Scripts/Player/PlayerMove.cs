using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 5.0f;

    private PlayerInputController mPlayerInputController;
    private Rigidbody2D mRigidbody;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();

        Debug.Assert(mRigidbody != null);
    }

    void Start()
    {
        mPlayerInputController = GameManager.gameManager.player.GetComponent<PlayerInputController>();

        Debug.Assert(mPlayerInputController != null);
    }

    private void FixedUpdate()
    {
        Vector2 direction = mPlayerInputController.Direction;

        direction = direction.normalized;

        mRigidbody.MovePosition(mRigidbody.position + Speed * Time.fixedDeltaTime * direction);
    }
}
