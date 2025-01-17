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

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;
        hinge.spring = spring;
    }

    void Update()
    {
        if (MtGameManager.gameState != "playing") return;

        JointSpring spring = hinge.spring;
        spring.targetPosition = Input.GetKey(inputKey) ? pressedPosition : restPosition;
        hinge.spring = spring;
    }
}
