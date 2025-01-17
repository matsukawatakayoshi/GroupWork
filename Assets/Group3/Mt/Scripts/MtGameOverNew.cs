using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MtGameOverNew : MonoBehaviour
{
    public GameObject GameOver;
    public TextMeshProUGUI statusText;

    private void OnTriggerEnter(Collider other)
    {
        statusText.enabled = true; // 表示にする
        if (other.gameObject.tag == "MtBall")
        {
            Destroy(other.gameObject);
            statusText.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        }
        MtGameManager.gameState = "gameend";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
