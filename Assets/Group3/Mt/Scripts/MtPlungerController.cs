using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtPlungerController : MonoBehaviour
{
    public float maxPullDistance = 1.0f;    // プランジャーを引ける最大距離
    public float maxForce = 5000f;          // 最大打ち出し力
    public KeyCode launchKey = KeyCode.Space; // 発射キー
    public float springForce = 10000f;

    private float pullDistance = 0f;         // 現在の引き具合
    private bool isPulling = false;          // 引いているか
    private Vector3 startPosition;           // プランジャーの初期位置
    private Rigidbody rb;                   // プランジャーのRigidbody

    [SerializeField] AudioSource PlungerSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        if (rb != null)
        {
            rb.mass = 1f;
            rb.drag = 0.1f;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation |
                           RigidbodyConstraints.FreezePositionX |
                           RigidbodyConstraints.FreezePositionY;
        }
        PlungerSound = GetComponent<AudioSource>();
        if (PlungerSound == null)
        {
            Debug.LogError("Don't Take care!");
        }
    }

    void Update()
    {
        if (MtGameManager.gameState != "playing") return;


        if (Input.GetKey(launchKey))
        {
            // スペースキーを押している間、プランジャーを引く
            isPulling = true;
            pullDistance = Mathf.Min(pullDistance + Time.deltaTime, maxPullDistance);
            UpdatePlungerPosition();
            if(PlungerSound != null)
            {
                PlungerSound.Play();
            }
        }
        else if (isPulling)
        {
            // キーを離したら発射
            isPulling = false;
            Release();
        }
    }

    void UpdatePlungerPosition()
    {
        // プランジャーの位置を更新
        Vector3 newPosition = startPosition;
        newPosition.z -= pullDistance;  // Z軸方向に引く（必要に応じて軸を変更）
        transform.position = newPosition;
    }

    void Release()
    {
        // ばね力による急速な戻り
        if (rb != null)
        {
            // 引いた距離に応じてばね力を計算
            float force = (pullDistance / maxPullDistance) * springForce;
            rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        }

        // ボールを打ち出す
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("MtBall"))
            {
                Rigidbody ballRb = col.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    // 引いた距離に基づいて力を計算（二次関数的に増加）
                    float force = (pullDistance / maxPullDistance) *
                                (pullDistance / maxPullDistance) * maxForce;
                    ballRb.AddForce(Vector3.forward * force, ForceMode.Impulse);
                }
            }
        }

        pullDistance = 0f;
    }
    void FixedUpdate()
    {
        if (!isPulling && rb != null)
        {
            // プランジャーが startPosition より前に行きすぎないように制限
            if (transform.position.z > startPosition.z)
            {
                Vector3 pos = transform.position;
                pos.z = startPosition.z;
                transform.position = pos;
                rb.velocity = Vector3.zero;
            }
        }
    }
}
