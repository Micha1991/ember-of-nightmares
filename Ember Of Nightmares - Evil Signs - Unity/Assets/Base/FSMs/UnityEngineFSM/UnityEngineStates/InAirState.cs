using UnityEngine;

public class InAirState : UnityEngineBaseState
{
    private bool countUp;
    private float weightIncreaser = 0.5f;

    public override void EnterState(UnityEngine_FSM unityEngine_FSM)
    {
        unityEngine_FSM.countTimeToAirborne = 0f;
        countUp = true;
    }

    public override bool Transition(UnityEngine_FSM unityEngine_FSM)
    {
        var isInAir = false;
        if (!unityEngine_FSM.isOnGround && unityEngine_FSM.rigidbodyMovesDown || unityEngine_FSM.rigidbodyMovesUp)
            isInAir = true;
        return isInAir;
    }

    public override void Update(UnityEngine_FSM unityEngine_FSM)
    {
        if (countUp)
            unityEngine_FSM.countTimeToAirborne += Time.deltaTime;
        if (unityEngine_FSM.countTimeToAirborne > unityEngine_FSM.maxTimeToAirborne)
        {
            countUp = false;
            unityEngine_FSM.currentEnumState = UnityEngineStates.ISINAIR;
            unityEngine_FSM.unityEngineStates.parameterValue = (int)unityEngine_FSM.currentEnumState;
            unityEngine_FSM.unityEngineStates.SetAnimatorValue(unityEngine_FSM.animator);
            unityEngine_FSM.isInAirTrigger.SetAnimatorValue(unityEngine_FSM.animator);

            unityEngine_FSM.animator.applyRootMotion = false;
            unityEngine_FSM.countTimeToAirborne = 0f;
            weightIncreaser = 0.5f;
        }
        if(unityEngine_FSM.currentEnumState == UnityEngineStates.ISINAIR)
        {

            unityEngine_FSM.animator.SetLayerWeight(unityEngine_FSM.engineForcedLayerNumber, weightIncreaser+=Time.deltaTime);
        }


        if (unityEngine_FSM.OnGroundState.Transition(unityEngine_FSM))
        {
            unityEngine_FSM.TransitionToState(unityEngine_FSM.OnGroundState);
        }
    }
}
