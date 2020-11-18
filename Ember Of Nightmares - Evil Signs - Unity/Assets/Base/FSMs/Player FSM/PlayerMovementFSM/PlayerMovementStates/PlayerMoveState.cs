using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerMovementBaseState
{
    public override void EnterState(PlayerMovement_FSM playerMovement_FSM)
    {
        playerMovement_FSM.currentStateEnum = PlayerMovementStates.MOVE;
        playerMovement_FSM.animator.SetInteger(playerMovement_FSM.nameOfStateInt, (int)PlayerMovementStates.MOVE);
    }

    public override bool Transition(PlayerMovement_FSM playerMovement_FSM)
    {
        var wantMove = false;
        if (playerMovement_FSM.userInputHorizontal != 0 || playerMovement_FSM.userInputVertical != 0 /*&& !playerMovement_FSM.jumpButtonDown*/)
            wantMove = true;
        return wantMove;
    }

    public override void Update(PlayerMovement_FSM playerMovement_FSM)
    {
        playerMovement_FSM.currentStateEnum = PlayerMovementStates.MOVE;

        if (playerMovement_FSM.PlayerIdleState.Transition(playerMovement_FSM))
        {
            playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerIdleState);
        }
        //if (playerMovement_FSM.PlayerMoveState.Transition(playerMovement_FSM))
        //{
        //    playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerMoveState);
        //}
        if (playerMovement_FSM.PlayerJumpState.Transition(playerMovement_FSM))
        {
            playerMovement_FSM.TransitionToState(playerMovement_FSM.PlayerJumpState);
        }
    }
}
