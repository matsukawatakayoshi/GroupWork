using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MtBumper : MonoBehaviour
{
    public float bounceForce = 1000f;
    public int scoreValue = 100;
    private MtScoreManagerNew scoreManager;

    private Material material;
    public float intensity = 0.5f;

    private Coroutine currentFadeCoroutine;

    [SerializeField] AudioSource BounceSound;
    //[SerializeField] AudioClip BounceSoundClip;

    void Start()
    {
        scoreManager = FindObjectOfType<MtScoreManagerNew>();

        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        BounceSound = GetComponent<AudioSource>();
        if (BounceSound == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MtBall"))
        {
            // ボールを跳ね返す
            Vector3 direction = collision.contacts[0].point - transform.position;
            collision.rigidbody.AddForce(direction.normalized * bounceForce);

            material.SetColor("_EmissionColor", Color.white * intensity);

            // スコア加算
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            // サウンド再生
            BounceSound.Play();
            //BounceSound.clip = BounceSoundClip;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MtBall"))
        {
            // 光を消す
            //material.SetColor("_EmissionColor", Color.black);
            currentFadeCoroutine = StartCoroutine(FadeOut());

        }
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.5f; // フェードアウトの時間（秒）
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentIntensity = Mathf.Lerp(intensity, 0, elapsed / duration);
            material.SetColor("_EmissionColor", Color.white * currentIntensity);
            yield return null;
        }

        // 完全に消灯
        material.SetColor("_EmissionColor", Color.black);
    }
}