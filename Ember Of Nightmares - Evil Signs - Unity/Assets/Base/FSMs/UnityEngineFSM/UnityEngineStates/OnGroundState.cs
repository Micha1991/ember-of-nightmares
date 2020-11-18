
public class OnGroundState : UnityEngineBaseState
{
    public override void EnterState(UnityEngine_FSM unityEngine_FSM)
    {
        if(unityEngine_FSM.currentEnumState == UnityEngineStates.ISINAIR)
        {
            unityEngine_FSM.currentEnumState = UnityEngineStates.ISONGROUND;
            unityEngine_FSM.unityEngineStates.parameterValue = (int)UnityEngineStates.ISONGROUND;
            unityEngine_FSM.isLandingTrigger.SetAnimatorValue(unityEngine_FSM.animator);
            unityEngine_FSM.unityEngineStates.SetAnimatorValue(unityEngine_FSM.animator);
            unityEngine_FSM.animator.applyRootMotion = true;

            unityEngine_FSM.countTimeToAirborne = 0f;
        }
    }

    public override bool Transition(UnityEngine_FSM unityEngine_FSM)
    {
        return unityEngine_FSM.isOnGround;
    }

    public override void Update(UnityEngine_FSM unityEngine_FSM)
    {
        unityEngine_FSM.currentEnumState = UnityEngineStates.ISONGROUND;

        if (unityEngine_FSM.InAirState.Transition(unityEngine_FSM))
        {
            unityEngine_FSM.TransitionToState(unityEngine_FSM.InAirState);
        }
    }
}
