using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MtFlipperController : MonoBehaviour
{
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;

    public KeyCode inputKey;
    private HingeJoint hinge;

    public bool isPressed = false;

   public AudioSource FlipperSound;
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;
        hinge.spring = spring;

        FlipperSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (MtGameManager.gameState != "playing") return;

        JointSpring spring = hinge.spring;
        bool isPressing = Input.GetKey(inputKey);

        spring.targetPosition = isPressing ? pressedPosition : restPosition;
        hinge.spring = spring;
        //spring.targetPosition = Input.GetKey(inputKey) ? pressedPosition : restPosition;
        //hinge.spring = spring;

        if (!isPressed && isPressing && FlipperSound != null)
        {
            FlipperSound.Play();
        }

        isPressed = isPressing;
    }

    void OnButtonClick()
    {

    }
}
