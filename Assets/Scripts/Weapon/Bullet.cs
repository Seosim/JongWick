using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Power = 50.0f;

    private Rigidbody2D mRigidBody;
    public void Initialize(Vector2 position, Vector2 direction, Quaternion rotation)
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        transform.position = position;
        transform.rotation = rotation;
        mRigidBody.AddForce(Power * direction, ForceMode2D.Impulse);
    }

    void Start()
    {
        if(mRigidBody == null)
            mRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    }
}
