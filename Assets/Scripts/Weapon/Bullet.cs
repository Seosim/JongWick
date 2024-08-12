using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Power = 50.0f;
    public float ActiveRange = 100.0f;

    private Rigidbody2D mRigidBody;
    private Vector2 mStartPosition;
    public void Initialize(Vector2 position, Vector2 direction, Quaternion rotation)
    {
        if (mRigidBody == null)
            mRigidBody = GetComponent<Rigidbody2D>();

        transform.position = position;
        mStartPosition = position;
        transform.rotation = rotation;

        mRigidBody.AddForce(Power * direction, ForceMode2D.Impulse);
    }

    void Start()
    {
        if(mRigidBody == null)
            mRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, mStartPosition);
        if (ActiveRange < distance)
            gameObject.SetActive(false);
    }
}
