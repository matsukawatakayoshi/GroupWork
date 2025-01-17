using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MtGameManager : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public static string gameState;

    // Start is called before the first frame update
    void Start()
    {

        TMP_Text textComponent = statusText.GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = "START!!";
        }

        gameState = "playing";
        // 時間差でstatusTextを非表示
        Invoke("HiddenStatusText", 2.0f);
    }

    void HiddenStatusText()
    {
        if (statusText != null)
        {
            statusText.enabled=false; // 非表示にする
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
