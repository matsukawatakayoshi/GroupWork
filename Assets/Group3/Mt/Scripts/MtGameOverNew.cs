using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MtGameOverNew : MonoBehaviour
{
    public GameObject GameOver;
    public TextMeshProUGUI statusText;

    [SerializeField] AudioSource GameOverSound;


    private void OnTriggerEnter(Collider other)
    {

        statusText.enabled = true; // 表示にする
        if (other.gameObject.tag == "MtBall")
        {
            Destroy(other.gameObject);
            statusText.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        }
        MtGameManager.gameState = "gameend";

        if(GameOverSound != null)
        { 
        
        GameOverSound.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameOverSound = GetComponent<AudioSource>();
        if (GameOverSound == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
