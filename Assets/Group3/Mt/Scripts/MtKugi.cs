using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtKugi : MonoBehaviour
{
    public float bounceForce = 1000f;
    [SerializeField] AudioSource KugiSound;

    // Start is called before the first frame update
    void Start()
    {
        KugiSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MtBall"))
        {
            // ボールを跳ね返す
            Vector3 direction = collision.contacts[0].point - transform.position;
            collision.rigidbody.AddForce(direction.normalized * bounceForce);

            KugiSound.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
