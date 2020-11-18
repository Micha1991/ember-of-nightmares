using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    #region Possible Controller Inputs and Names in Input Manager
    [Header("A")]
    public bool uiA;
    public string nameOf00;
    [Space(10)]

    [Header("B")]
    public bool uiB;
    public string nameOf01;
    [Space(10)]

    [Header("X")]
    public bool uiX;
    public string nameOf02;
    [Space(10)]

    [Header("Y")]
    public bool uiY;
    public string nameOf03;
    [Space(10)]

    [Header("Left Bumper")]
    public bool uiLeftBumper;
    public string nameOf04;
    [Space(10)]

    [Header("Right Bumper")]
    public bool uiRightBumper;
    public string nameOf05;
    [Space(10)]

    [Header("Back")]
    public bool uiBack;
    public string nameOf06;
    [Space(10)]

    [Header("Start")]
    public bool uiStart;
    public string nameOf07;
    [Space(10)]

    [Header("Left Stick Button")]
    public bool uiLeftStickButton;
    public string nameOf08;
    [Space(10)]

    [Header("Right Stick Button")]
    public bool uiRightStickButton;
    public string nameOf09;
    [Space(10)]

    [Header("Left Stick Horizontal")]
    [Range(-1,1)]
    public float uiLeftStickHorizontal;
    public string nameOf10;
    [Space(10)]

    [Header("Left Stick Vertical")]
    [Range(-1, 1)]
    public float uiLeftStickVertical;
    public string nameOf11;
    [Space(10)]

    [Header("Right Stick Horizontal")]
    [Range(-1, 1)]
    public float uiRightStickHorizontal;
    public string nameOf12;
    [Space(10)]

    [Header("Right Stick Vertical")]
    [Range(1, -1)]
    public float uiRightStickVertical;
    public string nameOf13;
    [Space(10)]

    [Header("DPAD Horizontal")]
    [Range(-1, 1)]
    public float uiDPADHorizontal;
    public string nameOf14;
    [Space(10)]

    [Header("DPAD Vertical")]
    [Range(-1, 1)]
    public float uiDPADVertical;
    public string nameOf15;
    [Space(10)]

    [Header("Left Trigger")]
    [Range(0, 1)]
    public float uiLeftTrigger;
    public string nameOf16;
    [Space(10)]

    [Header("Right Trigger")]
    [Range(0, 1)]
    public float uiRightTrigger;
    public string nameOf17;
    [Space(10)]

    [Header("Left Trigger Shared Axis")]
    [Range(0,1)]
    public float uiLeftTriggerSharedAxis;
    public string nameOf18;
    [Space(10)]

    [Header("Right Trigger Shared Axis")]
    [Range(0, -1)]
    public float uiRightTriggerSharedAxis;
    public string nameOf19;
    #endregion

    #region Camera Variables
    public Camera mainCamera;
    public Vector3 cameraForward;
    public Vector3 cameraRight;
    #endregion

    #region extended User Inputs
    [Header("User Input Vectors relativ to Camera View")]
    public Vector3 relativLeftStickInputVector;
    public Vector3 relativRightStickInputVector;
    public bool drawVectors;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiA = Input.GetButton(nameOf00);
        uiB = Input.GetButton(nameOf01);
        uiX = Input.GetButton(nameOf02);
        uiY = Input.GetButton(nameOf03);
        uiLeftBumper = Input.GetButton(nameOf04);
        uiRightBumper = Input.GetButton(nameOf05);
        uiBack = Input.GetButton(nameOf06);
        uiStart = Input.GetButton(nameOf07);
        uiLeftStickButton = Input.GetButton(nameOf08);
        uiRightStickButton = Input.GetButton(nameOf09);


        uiLeftStickHorizontal = Input.GetAxis(nameOf10);
        uiLeftStickVertical = Input.GetAxis(nameOf11);
        uiRightStickHorizontal = Input.GetAxis(nameOf12);
        uiRightStickVertical = Input.GetAxis(nameOf13);
        uiDPADHorizontal = Input.GetAxis(nameOf14);
        uiDPADVertical = Input.GetAxis(nameOf15);
        uiLeftTrigger = Input.GetAxis(nameOf16);
        uiRightTrigger = Input.GetAxis(nameOf17);
        uiLeftTriggerSharedAxis = Input.GetAxis(nameOf18);
        uiRightTriggerSharedAxis = Input.GetAxis(nameOf19);

        cameraForward = mainCamera.transform.forward;
        cameraRight = mainCamera.transform.right;
    }

    private void LateUpdate()
    {
        relativLeftStickInputVector = StickToWordPosition(uiLeftStickVertical, uiLeftStickHorizontal);
        relativRightStickInputVector = StickToWordPosition(uiRightStickVertical, uiRightStickHorizontal);

        if (drawVectors)
        {
            Debug.DrawRay(this.gameObject.transform.position, relativRightStickInputVector, Color.red);
            Debug.DrawRay(this.gameObject.transform.position, relativLeftStickInputVector, Color.blue);
        }
    }

    private Vector3 StickToWordPosition(float stickVertical, float stickHorizontal)
    {
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        return cameraForward * stickVertical + cameraRight * stickHorizontal;
    }
}
