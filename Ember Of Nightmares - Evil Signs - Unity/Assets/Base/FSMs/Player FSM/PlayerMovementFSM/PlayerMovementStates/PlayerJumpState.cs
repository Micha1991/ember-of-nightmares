using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerJumpState : PlayerMovementBaseState
{
    public override void EnterState(PlayerMovement_FSM playerMovement_FSM)
    {
        playerMovement_FSM.currentStateEnum = PlayerMovementStates.JUMP;
        playerMovement_FSM.animator.SetInteger(playerMovement_FSM.nameOfStateInt, (int)PlayerMovementStates.JUMP);
        playerMovement_FSM.animator.SetTrigger(playerMovement_FSM.nameOfJumpTrigger);
    }

    public override bool Transition(PlayerMovement_FSM playerMovement_FSM)
    {
        var wantJump = false;
        if (playerMovement_FSM.jumpButtonDown)
            wantJump = true;
        return wantJump;
    }

    public override void Update(PlayerMovement_FSM playerMovement_FSM)
    {
        playerMovement_FSM.currentStateEnum = PlayerMovementStates.JUMP;
        if (playerMovement_FSM.PlayerIdleState.Transition(playerMovement_FSM))
        {
            playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerIdleState);
        }
        if (playerMovement_FSM.PlayerMoveState.Transition(playerMovement_FSM))
        {
            playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerMoveState);
        }
        //if (playerMovement_FSM.PlayerJumpState.Transition(playerMovement_FSM))
        //{
        //    playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerJumpState);
        //}
    }
}
