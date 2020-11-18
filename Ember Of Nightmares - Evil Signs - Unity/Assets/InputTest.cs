using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public Animator animator;
    public string nameOfSpeedFloat;
    public string nameOfDirectionFloat;

    public float userInputHorizontal;
    public float userInputVertical;

    public float sprintButton;
    public float speed;
    public bool setSprint;
    public float directionDampTime = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        userInputHorizontal = Input.GetAxis("Horizontal");
        userInputVertical = Input.GetAxis("Vertical");
        sprintButton = Input.GetAxis("Left Shift");

        if (sprintButton > 0 && (Mathf.Abs(userInputHorizontal) > 0.8f || Mathf.Abs(userInputVertical) > 0.8f))
            setSprint = true;
        else if (Mathf.Abs(userInputHorizontal) < 0.5f && Mathf.Abs(userInputVertical) < 0.5f)
            setSprint = false;
        if (setSprint)
            speed = new Vector2(userInputHorizontal, userInputVertical).normalized.sqrMagnitude + 1f;
        else if (!setSprint)
            speed = new Vector2(userInputHorizontal, userInputVertical).normalized.sqrMagnitude;

        animator.SetFloat(nameOfSpeedFloat, speed);
        animator.SetFloat(nameOfDirectionFloat, userInputHorizontal, directionDampTime, Time.deltaTime);
    }
}
