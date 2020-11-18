using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RawBaseState
{
    //Context: What to do, when we in this state
    public abstract void EnterState(RawFSM rawFSM);
    //Called in the Update. From here the state can chaned to another state, if the conditions are right
    public abstract void Update(RawFSM rawFSM);
    //Transition to this state
    public abstract bool Transition(RawFSM rawFSM);
}
