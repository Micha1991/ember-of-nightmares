using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityEngineBaseState
{
    //Context: What to do, when we in this state
    public abstract void EnterState(UnityEngine_FSM unityEngine_FSM);
    //Called in the Update. From here the state can chaned to another state, if the conditions are right
    public abstract void Update(UnityEngine_FSM unityEngine_FSM);
    //Transition to this state
    public abstract bool Transition(UnityEngine_FSM unityEngine_FSM);
}
