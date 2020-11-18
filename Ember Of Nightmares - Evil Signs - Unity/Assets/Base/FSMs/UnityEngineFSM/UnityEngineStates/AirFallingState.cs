
//using UnityEngine;

//public class AirFallingState : UnityEngineBaseState
//{
//    private bool countUp;
//    public override void EnterState(UnityEngine_FSM unityEngine_FSM)
//    {

//        unityEngine_FSM.fallingTime = 0f;
//        unityEngine_FSM.rinsingTime = 0f;
//        countUp = true;
//    }

//    public override bool Transition(UnityEngine_FSM unityEngine_FSM)
//    {
//        var isAirFalling = false;
//        if (!unityEngine_FSM.isOnGround && unityEngine_FSM.rigidbodyMovesDown)
//            isAirFalling = true;
//        return isAirFalling;
//    }

//    public override void Update(UnityEngine_FSM unityEngine_FSM)
//    {
        
//        if (countUp)
//            unityEngine_FSM.fallingTime += Time.deltaTime;
//        if (unityEngine_FSM.fallingTime > unityEngine_FSM.timeToAirFallingRising)
//        {
//            countUp = false;
//            unityEngine_FSM.currentEnumState = UnityEngineStates.ISAIRFALLING;
//            unityEngine_FSM.animator.SetTrigger(unityEngine_FSM.nameOfFallingTrigger);
//            unityEngine_FSM.animator.SetInteger(unityEngine_FSM.nameOfStateInt, (int)UnityEngineStates.ISAIRFALLING);
//            unityEngine_FSM.animator.applyRootMotion = false;
//            unityEngine_FSM.fallingTime = 0f;
//        }

//        if (unityEngine_FSM.OnGroundState.Transition(unityEngine_FSM))
//        {
//            unityEngine_FSM.TransitionToState(unityEngine_FSM.OnGroundState);
//        }
//        if (unityEngine_FSM.AirRisingState.Transition(unityEngine_FSM))
//        {
//            unityEngine_FSM.TransitionToState(unityEngine_FSM.AirRisingState);
//        }
//        //if (unityEngine_FSM.AirFallingState.Transition(unityEngine_FSM))
//        //{
//        //    unityEngine_FSM.TransitionToState(unityEngine_FSM.AirFallingState);
//        //}
//    }
//}
