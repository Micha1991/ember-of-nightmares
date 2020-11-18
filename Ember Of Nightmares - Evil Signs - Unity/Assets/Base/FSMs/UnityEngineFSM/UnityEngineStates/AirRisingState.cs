
//using UnityEngine;

//public class AirRisingState : UnityEngineBaseState
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
//        var isAirRising = false;
//        if (!unityEngine_FSM.isOnGround && unityEngine_FSM.rigidbodyMovesUp)
//            isAirRising = true;
//        return isAirRising;
//    }

//    public override void Update(UnityEngine_FSM unityEngine_FSM)
//    {
//        if(countUp)
//            unityEngine_FSM.rinsingTime += Time.deltaTime;
//        if(unityEngine_FSM.rinsingTime > unityEngine_FSM.timeToAirFallingRising)
//        {
//            countUp = false;
//            unityEngine_FSM.currentEnumState = UnityEngineStates.ISAIRRISING;
//            unityEngine_FSM.animator.SetTrigger(unityEngine_FSM.nameOfRisingTrigger);
//            unityEngine_FSM.animator.SetInteger(unityEngine_FSM.nameOfStateInt, (int)UnityEngineStates.ISAIRRISING);
//            unityEngine_FSM.animator.applyRootMotion = false;
//            unityEngine_FSM.rinsingTime = 0f;
//        }

//        unityEngine_FSM.currentEnumState = UnityEngineStates.ISAIRRISING;

//        if (unityEngine_FSM.OnGroundState.Transition(unityEngine_FSM))
//        {
//            unityEngine_FSM.TransitionToState(unityEngine_FSM.OnGroundState);
//        }
//        //if (unityEngine_FSM.AirRisingState.Transition(unityEngine_FSM))
//        //{
//        //    unityEngine_FSM.TransitionToState(unityEngine_FSM.AirRisingState);
//        //}
//        if (unityEngine_FSM.AirFallingState.Transition(unityEngine_FSM))
//        {
//            unityEngine_FSM.TransitionToState(unityEngine_FSM.AirFallingState);
//        }

//    }
//}
