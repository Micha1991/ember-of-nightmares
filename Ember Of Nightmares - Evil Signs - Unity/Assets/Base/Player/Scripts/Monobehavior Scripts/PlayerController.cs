using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerComponentsHandler playerComponentsHandler;

    float inputX;
    float inputZ;
    bool isInMenu;
    float leftShift;

    // Start is called before the first frame update
    void Start()
    {
        playerComponentsHandler = GetComponent<PlayerComponentsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserRequest();
    }

    #region own methods

    void CheckUserRequest()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        leftShift = Input.GetAxis("Left Shift");
        isInMenu = playerComponentsHandler.playerCharacteristics.isInMenu;

        if (!isInMenu && (inputX != 0 || inputZ != 0))
            MovePlayer();
    }
    void MovePlayer()
    {
        //Vertical left right x- Axses
        //Horizontal forward backward z-Axes
        //1. forward
        //2. forward left
        //3. forward right
        //4. left
        //5. right
        //6. backward left
        //7. backward right
        //8. backward
        var maxSpeed = 0f;

        //1. forward
        if (inputX == 0  && inputZ > 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesFordward = true;
            if(leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunForwardSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkForwardSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesFordward = false;
        }
        //2. forward left
        if (inputX < 0 && inputZ > 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackwardLeft = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunVerticalFordwardSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkVerticalFordwardSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackwardLeft = false;
        }
        //3. forward right
        if (inputX > 0 && inputZ > 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesFordwardRight = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunForwardSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkForwardSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesFordwardRight = false;
        }
        //4. left
        if (inputX < 0 && inputZ == 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesLeft = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunHorizontalSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkHorizontalSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesLeft = false;
        }
        //5. right
        if (inputX > 0 && inputZ == 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesRight = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunHorizontalSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkHorizontalSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesRight = false;
        }
        //6. backward left
        if (inputX < 0 && inputZ < 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackwardLeft = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunVerticalBackwardSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkVerticalBackwardSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackwardLeft = false;
        }
        //7. backward right
        if (inputX > 0 && inputZ < 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackwardRight = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunVerticalBackwardSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkVerticalBackwardSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackwardRight = false;
        }
        //8. backward
        if (inputX < 0 && inputZ == 0)
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackward = true;
            if (leftShift > 0)
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxRunBackwardSpeed;
            }
            else
            {
                maxSpeed = playerComponentsHandler.playerCharacteristics.maxWalkBackwardSpeed;
            }
        }
        else
        {
            playerComponentsHandler.playerCharacteristics.userMovesBackward = false;
        }

        var moveDir = new Vector3(inputX, 0, inputZ);
        playerComponentsHandler.playerRB.velocity += moveDir * Time.deltaTime * maxSpeed;
    }
    #endregion
}
