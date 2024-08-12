using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    enum eMouseType
    {
        Left,
        Right,
        Wheel
    }
    public Action aLeftMouseDown;
    public Action aLeftMouseUp;
    public Action aRightMouseDown;
    public Action aRKeyDown;

    public Vector2 Direction { get { return mDirection; } private set { Direction = mDirection; } }

    private Vector2 mDirection;

    private void Awake()
    {
        mDirection = Vector2.zero;
    }

    private void Update()
    {
        //KeyDown
        if(Input.GetKeyDown(KeyCode.A))
        {
            --mDirection.x;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ++mDirection.x;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ++mDirection.y;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            --mDirection.y;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            aRKeyDown?.Invoke();
        }

        //KeyUp
        if (Input.GetKeyUp(KeyCode.A))
        {
            ++mDirection.x;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            --mDirection.x;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            --mDirection.y;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ++mDirection.y;
        }

        //Mouse
        if (Input.GetMouseButtonDown((int)eMouseType.Left))
        {
            aLeftMouseDown?.Invoke();
        }
        if (Input.GetMouseButtonUp((int)eMouseType.Left))
        {
            aLeftMouseUp?.Invoke();
        }
        if (Input.GetMouseButtonDown((int)eMouseType.Right))
        {
            aRightMouseDown?.Invoke();
        }
    }
}
