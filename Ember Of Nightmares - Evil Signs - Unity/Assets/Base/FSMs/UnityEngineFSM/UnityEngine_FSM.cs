using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityEngine_FSM : MonoBehaviour
{
    public RigidbodyDefinerMain rigidbodyDefiner;
    public List<NerveGroundCheck> groundChecks = new List<NerveGroundCheck>();
    public Animator animator;

    [Header("Animator Parameters")]
    public AnimatorTriggerParameter isInAirTrigger = new AnimatorTriggerParameter();
    public AnimatorTriggerParameter isLandingTrigger = new AnimatorTriggerParameter();
    public AnimatorIntParameter unityEngineStates = new AnimatorIntParameter();
    public AnimatorFloatParameter rigidbodyVelocityY = new AnimatorFloatParameter();
    public int engineForcedLayerNumber;

    public UnityEngineStates currentEnumState = new UnityEngineStates();

    private UnityEngineBaseState currentState;
    public readonly OnGroundState OnGroundState = new OnGroundState();
    public readonly InAirState InAirState = new InAirState();

    #region parameters
    [NonSerialized] public bool rigidbodyMovesUp;
    [NonSerialized] public bool rigidbodyMovesDown;
    [NonSerialized] public bool isOnGround;
    public float countTimeToAirborne;
    public float maxTimeToAirborne = 0.3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(OnGroundState);
        rigidbodyVelocityY.NameToHashID();
        isInAirTrigger.NameToHashID();
        isLandingTrigger.NameToHashID();
        unityEngineStates.NameToHashID();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshParameters();
        currentState.Update(this);
    }

    public void TransitionToState(UnityEngineBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        ResetFreezeRigidbodyRotation();
    }

    void RefreshParameters()
    {
        rigidbodyMovesUp = rigidbodyDefiner.movesYpos;
        rigidbodyMovesDown = rigidbodyDefiner.movesYneg;
        //If there trues in the list, the list is not empty and so the isOnGround is not false
        isOnGround = groundChecks.Where(v => v.isOnGround == true).ToList().Count > 0;
        rigidbodyVelocityY.parameterValue = rigidbodyDefiner.virtualYvel;
        unityEngineStates.parameterValue = (int)currentEnumState;
    }
    void ResetFreezeRigidbodyRotation()
    {
        if (rigidbodyDefiner.definerRigidbody.angularVelocity.y != 0)
        {
            rigidbodyDefiner.definerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbodyDefiner.definerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
public enum UnityEngineStates
{
    ISONGROUND = 10,
    ISAIRRISING = 20,
    ISAIRFALLING = 30,
    ISINAIR = 40,
}
