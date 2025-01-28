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
    private Button rButton; // <<<ボタン（前のタイトル画面にもどる）
    [SerializeField]
    private Button pButton; // Xボタン(リスタート←ポーズに変更予定）
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Button retireButton;
    [SerializeField]
    private Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (rButton != null )
        {
            rButton.onClick.AddListener(ChangeScene);// <<<ボタン前のシーンへ
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);// リスタートボタンを消す
        }

        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(false);
        }

        if (pButton != null) // ポーズに変更予定
        {
            pButton.onClick.AddListener(PauseButton);
        }


        TMP_Text textComponent = statusText.GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = "START!!\nL>A\nR>D\nBall>Sp";
        }


        gameState = "playing";
        // 時間差でstatusTextを非表示
        Invoke("HiddenStatusText", 2.0f);

        pausePanel.gameObject.SetActive(false);

        if(retireButton != null)
        {
            retireButton.onClick.AddListener(RetireButton);
        }

        if(continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueButton);
        }
    }

    void HiddenStatusText()
    {
        if (statusText != null)
        {
            statusText.enabled=false; // 非表示にする
        }

    }

    // ポーズボタンメソッド
    public void PauseButton()
    {
        pausePanel.gameObject.SetActive(true);
        //GameObject pausePanel = GameObject.Find("MtPause");
        //pausePanel.GetComponent<Canvas>().sortingOrder = 10;

        Time.timeScale = 0;

        //BGM停止
        //AudioSource soundPlayer = GetComponent<AudioSource>();
        //soundPlayer.Stop();
    }

    public void ContinueButton()
    {
        pausePanel.gameObject.SetActive(false);

        Time.timeScale = 1;

        ////BGM再生
        //AudioSource soundPlayer = GetComponent<AudioSource>();
        //soundPlayer.Play();
    }

    public void RetireButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MtPinballMain");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "gameend")
        {
            ShowRestartButton();
            ShowExitButton();
        }

        if (Time.timeScale == 0) return;
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
        SceneManager.LoadScene("Title");
    }
}
