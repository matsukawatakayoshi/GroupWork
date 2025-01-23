using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtBallController : MonoBehaviour
{
    public float maxVelocity = 30f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 最大速度を制限
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}