using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement_FSM : MonoBehaviour
{
    [Header("Required Components")]
    public UserInput userInput;

    #region UserSettings
    [Header("Animator Settings")]
    public Animator animator;
    public string nameOfJumpTrigger;
    public string nameOfInputXFloat;
    public string nameOfInputZFloat;
    public string nameOfSpeedFloat;
    public string nameOfDirectionFloat;
    //public string nameOfSprintFloat;
    public string nameOfStateInt;
    public string nameOfBattleStateBool;
    public LayerMask whatIsTagret;
    [TagSelector] private string TagFilter = "";

    [TagSelector] public string[] targetsByTag = new string[] { };
    #endregion

    #region States
    [Header("State Settings")]
    public PlayerMovementStates currentStateEnum = new PlayerMovementStates();

    private PlayerMovementBaseState currentState;
    public readonly PlayerIdleState PlayerIdleState = new PlayerIdleState();
    public readonly PlayerMoveState PlayerMoveState = new PlayerMoveState();
    public readonly PlayerJumpState PlayerJumpState = new PlayerJumpState();
    #endregion

    #region parameters
    [Header("Parameters")]
    [NonSerialized] public float userInputHorizontal;
    [NonSerialized] public float userInputVertical;

    [NonSerialized] public bool jumpButtonDown;
    [NonSerialized] public float sprintButton;
    public bool battleMode;
    [NonSerialized] public GameObject target;
    [NonSerialized] public Vector3 lookDirection;
    private float angleToLookDirection;
    #endregion

    #region Helper Variables
    [NonSerialized] public float speed;
    [NonSerialized] private float directionDampTime = 0.25f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(PlayerIdleState);
    }

    private void Update()
    {
        RefreshParameters();
        currentState.Update(this);
        GetTarget();
        RefreshLookDirection();
        RotateToLookDirection();
        RefreshAnimatorVariables();
    }

    public void TransitionToState(PlayerMovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void RefreshParameters()
    {
        RefreshUserInput();
    }
    void RefreshUserInput()
    {
        userInputHorizontal = Input.GetAxis("Horizontal");
        userInputVertical = Input.GetAxis("Vertical");
        jumpButtonDown = Input.GetButtonDown("Jump");
        sprintButton = Input.GetAxis("Left Shift");
        speed = new Vector2(userInputHorizontal, userInputVertical).sqrMagnitude;
        if(sprintButton > 0)
        {
            speed += sprintButton;
        }
    }
    void GetTarget()
    {
        //ToDo: Make the target selection with a Raycast from the Player in forward direction only with one button
        if (Input.GetMouseButtonDown(0) && Input.GetButton("Left Alt"))
        {
            battleMode = false;
            target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, whatIsTagret))
            {
                battleMode = true;
                target = hit.transform.gameObject;
                //If tags are selected and no object with some of this tags are setted, set back
                if (targetsByTag.Count() > 0 && !targetsByTag.Contains(hit.transform.tag))
                {
                    target = null;
                    battleMode = false;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            target = null;
            battleMode = false;
        }
    }
    void RefreshLookDirection()
    {
        if(target != null)
        {
            Vector3 targetPlaneVec = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            lookDirection = targetPlaneVec - transform.position;
            angleToLookDirection = Vector3.SignedAngle(lookDirection, transform.forward, Vector3.up);
        }
        else if(target == null)
        {
            Vector3 camFordward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camFordward.y = 0f;
            camRight.y = 0f;
            camFordward = camFordward.normalized;
            camRight = camRight.normalized;

            lookDirection = camFordward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal");
        }
    }
    void RotateToLookDirection()
    {
        Vector3 roundedLookDirection = new Vector3(Mathf.Round(lookDirection.x), Mathf.Round(lookDirection.y), Mathf.Round(lookDirection.z));
        if (roundedLookDirection != Vector3.zero)
        {
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, Quaternion.LookRotation(lookDirection), 0.5f);
        }
    }
    void RefreshAnimatorVariables()
    {
        //animator.SetFloat(nameOfInputXFloat, userInputHorizontal);
        //animator.SetFloat(nameOfInputZFloat, userInputVertical);
        animator.SetFloat(nameOfSpeedFloat, speed);
        animator.SetFloat(nameOfDirectionFloat, userInputHorizontal, directionDampTime, Time.deltaTime);
        //animator.SetBool(nameOfBattleStateBool, battleMode);
    }
}
public enum PlayerMovementStates
{
    IDLE = 10,
    MOVE = 20,
    JUMP = 30
}