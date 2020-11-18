using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerMovementBaseState
{
    public override void EnterState(PlayerMovement_FSM playerMovement_FSM)
    {
        playerMovement_FSM.currentStateEnum = PlayerMovementStates.IDLE;
        playerMovement_FSM.animator.SetInteger(playerMovement_FSM.nameOfStateInt, (int)PlayerMovementStates.IDLE);
    }

    public override bool Transition(PlayerMovement_FSM playerMovement_FSM)
    {
        var wantIdle = false;
        if (playerMovement_FSM.userInputVertical == 0 && playerMovement_FSM.userInputHorizontal == 0 && !playerMovement_FSM.jumpButtonDown)
            wantIdle = true;
        return wantIdle;
    }

    public override void Update(PlayerMovement_FSM playerMovement_FSM)
    {

        playerMovement_FSM.currentStateEnum = PlayerMovementStates.IDLE;
        //if (playerMovement_FSM.PlayerIdleState.Transition(playerMovement_FSM))
        //{
        //    playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerIdleState);
        //}
        if (playerMovement_FSM.PlayerMoveState.Transition(playerMovement_FSM))
        {
            playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerMoveState);
        }
        if (playerMovement_FSM.PlayerJumpState.Transition(playerMovement_FSM))
        {
            playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerJumpState);
        }
    }
}
