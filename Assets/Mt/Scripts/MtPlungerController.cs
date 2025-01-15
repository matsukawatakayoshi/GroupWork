using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtPlungerController : MonoBehaviour
{
    public float maxPullDistance = 1.0f;    // プランジャーを引ける最大距離
    public float maxForce = 5000f;          // 最大打ち出し力
    public KeyCode launchKey = KeyCode.Space; // 発射キー

    private float pullDistance = 0f;         // 現在の引き具合
    private bool isPulling = false;          // 引いているか
    private Vector3 startPosition;           // プランジャーの初期位置
    private Rigidbody rb;                   // プランジャーのRigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(launchKey))
        {
            // スペースキーを押している間、プランジャーを引く
            isPulling = true;
            pullDistance = Mathf.Min(pullDistance + Time.deltaTime, maxPullDistance);
            UpdatePlungerPosition();
        }
        else if (isPulling)
        {
            // キーを離したら発射
            isPulling = false;
            Launch();
        }

        // プランジャーをゆっくり元の位置に戻す
        if (!isPulling && pullDistance > 0)
        {
            pullDistance = Mathf.Max(0, pullDistance - Time.deltaTime * 2);
            UpdatePlungerPosition();
        }
    }

    void UpdatePlungerPosition()
    {
        // プランジャーの位置を更新
        Vector3 newPosition = startPosition;
        newPosition.z -= pullDistance;  // Z軸方向に引く（必要に応じて軸を変更）
        transform.position = newPosition;
    }

    void Launch()
    {
        // プランジャー周辺のボールを検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Ball"))
            {
                Rigidbody ballRb = col.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    // 引いた距離に応じた力でボールを打ち出す
                    float force = (pullDistance / maxPullDistance) * maxForce;
                    ballRb.AddForce(Vector3.forward * force, ForceMode.Impulse);
                }
            }
        }
    }
}
