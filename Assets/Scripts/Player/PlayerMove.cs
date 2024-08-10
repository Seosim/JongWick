using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 5.0f;
    public float DashPower = 300.0f;

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
        mPlayerInputController.RightMouseDown += Dash;

        Debug.Assert(mPlayerInputController != null);
    }

    private void FixedUpdate()
    {
        Vector2 direction = mPlayerInputController.Direction;
        direction = direction.normalized;
        mRigidbody.AddForce(direction * Speed, ForceMode2D.Force);

        if(mRigidbody.velocity != Vector2.zero)
        {
            mRigidbody.velocity = Vector2.Lerp(mRigidbody.velocity, Vector2.zero, 9.8f * Time.deltaTime);
        }
    }

    private void Dash()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dashDirection = (mousePosition - playerScreenPosition).normalized;

        mRigidbody.AddForce(dashDirection * DashPower, ForceMode2D.Impulse);
    }
}
