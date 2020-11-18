using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawFSM : MonoBehaviour
{

    //Enum with all the states -> public RawStates rawStates = new RawStates();
    //State to switch off -> private RawBaseState currentState;

    //States i want to use -> public readonly SomeState SomeState = new SomeState();
    //States i want to use -> public readonly AnotherState AnotherState = new AnotherState();

    // Start is called before the first frame update
    void Start()
    {
        //The init state -> TransitionToState(SomeState);
    }

    // Update is called once per frame
    void Update()
    {
        //Update the states -> currentState.Update(this);
    }

    //public void TransitionToState(PlayerBaseState state)
    //{
    //    currentState = state;
    //    currentState.EnterState(this);
    //}
}
//public enum RawStates
//{
//    rawOne = 10,
//    rawTwo = 20,
//}