using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MtFlipperControllerL : MonoBehaviour
{
    
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;

    public KeyCode inputKey;
    private HingeJoint hinge;

    public bool isPressed = false;
    public bool isLeftFlipper;

    private bool isButtonPressed = false;

    public AudioSource FlipperSound;

    [SerializeField]
    private Button flipperButtonDown;
    //[SerializeField]
    //private Button flipperButtonUp;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;
        hinge.spring = spring;

        FlipperSound = GetComponent<AudioSource>();

        if (flipperButtonDown != null)
        {
            var flipperButtonDown = GameObject.Find("MtLeftFlipperButton").GetComponent<Button>();
            //flipperButtonDown.onClick.AddListener(OnPointerDown);
            flipperButtonDown.onClick.AddListener(() => { isButtonPressed = !isButtonPressed; });
        }
    }

    void Update()
    {
        if (MtGameManager.gameState != "playing") return;

        JointSpring spring = hinge.spring;
        bool isPressing = Input.GetKey(inputKey) || isButtonPressed; ;
        //bool isPressing = Input.GetKey(inputKey) /*|| EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0) && gameObject.name == "MtFlipperLeft1"*/;

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

    public void OnPointerDown(/*PointerEventData eventData*/)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(/*PointerEventData eventData*/)
    {
        isButtonPressed = false;
    }

    public void Tekito()
    {

    }

}