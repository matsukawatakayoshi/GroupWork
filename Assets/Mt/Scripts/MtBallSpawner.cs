using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;          // ボールのプレハブ
    public Transform spawnPoint;           // ボールの生成位置
    public KeyCode spawnKey = KeyCode.R;   // ボール再生成キー

    private GameObject currentBall;

    void Start()
    {
        SpawnNewBall();
    }

    void Update()
    {
        // Rキーでボールを再生成
        if (Input.GetKeyDown(spawnKey))
        {
            if (currentBall != null)
            {
                Destroy(currentBall);
            }
            SpawnNewBall();
        }
    }

    void SpawnNewBall()
    {
        currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }
}
