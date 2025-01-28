using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MtFlipperController : MonoBehaviour/*, IPointerDownHandler, IPointerUpHandler*/
{
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;

    public KeyCode inputKey;
    private HingeJoint hinge;

    public bool isPressed = false;
    public bool isLeftFlipper;

    //private bool isButtonPressed = false;

    public AudioSource FlipperSound;

    //[SerializeField]
    //private Button flipperButtonDown;

    [SerializeField]
    private bool flipperButtonDownFlag = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;
        hinge.spring = spring;

        FlipperSound = GetComponent<AudioSource>();

        //if (flipperButtonDown != null)
        //{
        //    flipperButtonDown = GameObject.Find("MtRightFlipperButton").GetComponent<Button>();
        //    flipperButtonDown.onClick.AddListener(() => { isButtonPressed = !isButtonPressed; });
        //}
    }

    void Update()
    {
        if (MtGameManager.gameState != "playing") return;

        JointSpring spring = hinge.spring;
        bool isPressing = Input.GetKey(inputKey) || flipperButtonDownFlag; ;

        spring.targetPosition = isPressing ? pressedPosition : restPosition;
        hinge.spring = spring;

        if (!isPressed && isPressing && FlipperSound != null)
        {
            FlipperSound.Play();
        }

        isPressed = isPressing;

        if (flipperButtonDownFlag)
        {
            // ボタンが押しっぱなしの状態の時にのみ「Hold」を表示する。
            //Debug.Log("Hold");
        }
    }

    // ボタンを押したときの処理
    public void OnButtonDown()
    {
        //Debug.Log("Down");
        flipperButtonDownFlag = true;
    }
    // ボタンを離したときの処理
    public void OnButtonUp()
    {
        //Debug.Log("Up");
        flipperButtonDownFlag = false;
    }


}
