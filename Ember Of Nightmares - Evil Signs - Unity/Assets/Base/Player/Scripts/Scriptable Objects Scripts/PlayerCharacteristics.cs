using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Player Characteristics", menuName = "Player/PlayerCharacteristics")]
public class PlayerCharacteristics : ScriptableObject
{
    public float health = 100;
    public float maxWalkForwardSpeed = 15;
    public float maxWalkVerticalFordwardSpeed = 10;
    public float maxWalkHorizontalSpeed = 8;
    public float maxWalkVerticalBackwardSpeed = 5;
    public float maxWalkBackwardSpeed = 10;
    public float maxRunForwardSpeed = 30;
    public float maxRunVerticalFordwardSpeed = 20;
    public float maxRunHorizontalSpeed = 16;
    public float maxRunVerticalBackwardSpeed = 10;
    public float maxRunBackwardSpeed = 16;
    public float maxRotateSpeed = 30;
    public float jumpForce = 300;
    public bool isInMenu = false;
    public bool userMovesFordward;
    public bool userMovesLeft;
    public bool userMovesRight;
    public bool userMovesFordwardLeft;
    public bool userMovesFordwardRight;
    public bool userMovesBackward;
    public bool userMovesBackwardLeft;
    public bool userMovesBackwardRight;
    public bool rbMovesFordward;
    public bool rbMovesLeft;
    public bool rbMovesRight;
    public bool rbMovesFordwardLeft;
    public bool rbmovesFordwardRight;
    public bool rbMovesBackward;
    public bool rbMovesBackwardLeft;
    public bool rbMovesBackwardRight;
    public bool rbMovesUp;
    public bool rbMovesDown;
    public bool isAtGround;
    public bool isAtWall;
    public bool isAtCeiling;
    public bool isInAir;
}
