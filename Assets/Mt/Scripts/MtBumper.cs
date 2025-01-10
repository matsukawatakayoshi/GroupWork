using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtBumper : MonoBehaviour
{
    public float bounceForce = 1000f;
    public int scoreValue = 100;
    private MtScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<MtScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // ボールを跳ね返す
            Vector3 direction = collision.contacts[0].point - transform.position;
            collision.rigidbody.AddForce(direction.normalized * bounceForce);

            // スコア加算
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }
        }
    }
}