using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFreeModeController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private UserInput userInput;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    [Header("Animation Control Parameters")]
    [SerializeField] private AnimatorFloatParameter movementSpeed = new AnimatorFloatParameter();
    [SerializeField] private AnimatorFloatParameter movementDirection = new AnimatorFloatParameter();
    [SerializeField] private AnimatorFloatParameter angleToDirection = new AnimatorFloatParameter();
    [SerializeField] private AnimatorBoolParameter freeModeActive = new AnimatorBoolParameter();
    [SerializeField] private AnimatorTriggerParameter jumpTrigger = new AnimatorTriggerParameter();
    [SerializeField] private bool sprint;
    public bool isTurningOrWhat;

    [Header("Animator State Infos")]
    [SerializeField] private AnimatorStateInfo isTurning = new AnimatorStateInfo();

    [Header("Help Variables")]
    private float elapsedJumpTime;
    [SerializeField] float jumpResetTime;


    // Start is called before the first frame update
    void Start()
    {
        freeModeActive.parameterValue = true;

        isTurning.NameToHashID();

        movementSpeed.NameToHashID();
        movementDirection.NameToHashID();
        angleToDirection.NameToHashID();
        freeModeActive.NameToHashID();
        jumpTrigger.NameToHashID();
    }

    // Update is called once per frame
    void Update()
    {
        if (freeModeActive.parameterValue)
        {
            if(!CheckIfTurningAnimation())
                angleToDirection.parameterValue = CalculateAngleToDirection(userInput.relativLeftStickInputVector);
            movementDirection.parameterValue = angleToDirection.parameterValue / 180;
            CalculateMovementSpeedValue(userInput.uiLeftStickHorizontal, userInput.uiLeftStickVertical, userInput.uiLeftStickButton);
            SetAnimatorParameters();
            LerpPlayerToMoveDirection();
            CheckJump();
        }

    }

    #region Own Methods
    private float CalculateAngleToDirection(Vector3 relUserInputDirection)
    {
        return Vector3.SignedAngle(player.forward, relUserInputDirection, Vector3.up);
    }

    private void CalculateMovementSpeedValue(float inputHor, float inputVer, bool sprintButton)
    {
        var rightStickMaginute = new Vector2(inputHor, inputVer).magnitude;
        if (sprintButton && rightStickMaginute > 0.9f)
            sprint = true;
        else if (rightStickMaginute < 0.95f)
            sprint = false;
        if (sprint)
            movementSpeed.parameterValue = rightStickMaginute + 0.2f;
        else if (!sprint)
            movementSpeed.parameterValue = rightStickMaginute;
    }

    private void SetAnimatorParameters()
    {
        movementSpeed.SetAnimatorValue(animator);
        movementDirection.SetAnimatorValue(animator);
        angleToDirection.SetAnimatorValue(animator);
        freeModeActive.SetAnimatorValue(animator);
    }

    bool CheckIfTurningAnimation()
    {
        isTurningOrWhat = animator.GetCurrentAnimatorStateInfo(isTurning.laynumber).tagHash == isTurning.hashId;

        if (isTurningOrWhat)
            return true;
        else return false;
    }

    void CheckJump()
    {
        elapsedJumpTime += Time.deltaTime;
        if (userInput.uiA && elapsedJumpTime > jumpResetTime )
        {
            jumpTrigger.SetAnimatorValue(animator);
            elapsedJumpTime = 0f;
        }
    }

    void LerpPlayerToMoveDirection()
    {
        //Set Trigger if Angels are in tolerance
        if(movementSpeed.parameterValue > 0.1)
        {
            if (Mathf.Abs(angleToDirection.parameterValue) < 90)
            {
                player.transform.forward = Vector3.Slerp(player.transform.forward, userInput.relativLeftStickInputVector, 0.1f);
            }
        }
    }
    #endregion
}

