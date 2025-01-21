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
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button xButton;

    // Start is called before the first frame update
    void Start()
    {
        if (xButton != null )
        {
            xButton.onClick.AddListener(ChangeScene);
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }

        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(false);
        }

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
        if (gameState == "gameend")
        {
            ShowRestartButton();
            ShowExitButton();
        }
    }

    private void ShowRestartButton()
    {
        // ボタンが非表示の時だけ処理を行う
        if (!restartButton.gameObject.activeSelf)
        {
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void ShowExitButton()
    {
        if (!exitButton.gameObject.activeSelf)
        {
            exitButton.gameObject.SetActive(true);
            exitButton.onClick.AddListener(ChangeScene);
        }
    }

    public void RestartGame()
    {
        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ChangeScene()
    {
        // 指名したシーンに移る
        SceneManager.LoadScene("MtKariTitle");
    }
}
