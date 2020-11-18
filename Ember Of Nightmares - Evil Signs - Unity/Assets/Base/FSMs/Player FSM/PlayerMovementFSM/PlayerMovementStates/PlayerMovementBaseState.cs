using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementBaseState
{
    //Context: What to do, when we in this state
    public abstract void EnterState(PlayerMovement_FSM playerMovement_FSM);
    //Called in the Update. From here the state can chaned to another state, if the conditions are right
    public abstract void Update(PlayerMovement_FSM playerMovement_FSM);
    //Transition to this state
    public abstract bool Transition(PlayerMovement_FSM playerMovement_FSM);

}
