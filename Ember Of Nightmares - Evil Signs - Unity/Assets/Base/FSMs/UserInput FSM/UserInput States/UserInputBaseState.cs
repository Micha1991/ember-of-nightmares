using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInputBaseState
{
    //Context: What to do, when we in this state
    public abstract void EnterState(UserInput_FSM userInput_FSM);
    //Called in the Update. From here the state can chaned to another state, if the conditions are right
    public abstract void Update(UserInput_FSM userInput_FSM);
    //Transition to this state
    public abstract bool Transition(UserInput_FSM userInput_FSM);
}
