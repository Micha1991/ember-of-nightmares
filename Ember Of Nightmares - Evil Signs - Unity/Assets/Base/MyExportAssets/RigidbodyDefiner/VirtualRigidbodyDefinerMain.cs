using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VirtualRigidbodyDefinerMain : MonoBehaviour
{
    //This component simulates gravity and gives a feedback of velocity of the aimobject

    public GameObject aimObject;

    public bool movesX;
    public bool movesY;
    public bool movesZ;

    public bool movesXpos;
    public bool movesXneg;
    public bool movesYpos;
    public bool movesYneg;
    public bool movesZpos;
    public bool movesZneg;

    public float virtualXvel;
    public float virtualYvel;
    public float virtualZvel;
    public float virtualYZSlope;

    private float borderVel = 0.005f;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetRBStates();
    }

    private void FixedUpdate()
    {
        CalculateVirtualVelocitys();
    }

    void SetRBStates()
    {
        if (aimObject != null)
        {
            //MoveX
            if (Mathf.Abs(virtualXvel) > borderVel)
            {
                movesX = true;
            }
            else movesX = false;
            if (virtualXvel > borderVel)
            {
                movesXpos = true;
            }
            else
            {
                movesXpos = false;
            }
            if (virtualXvel < -borderVel)
            {
                movesXneg = true;
            }
            else
            {
                movesXneg = false;
            }

            //MoveY
            if (Mathf.Abs(virtualYvel) > borderVel)
            {
                movesY = true;
            }
            else movesY = false;
            if (virtualYvel > borderVel)
            {
                movesYpos = true;
            }
            else
            {
                movesYpos = false;
            }
            if (virtualYvel < -borderVel)
            {
                movesYneg = true;
            }
            else
            {
                movesYneg = false;
            }

            //MoveZ
            if (Mathf.Abs(virtualZvel) > borderVel)
            {
                movesZ = true;
            }
            else movesZ = false;
            if (virtualZvel > borderVel)
            {
                movesZpos = true;
            }
            else
            {
                movesZpos = false;
            }
            if (virtualZvel < -borderVel)
            {
                movesZneg = true;
            }
            else
            {
                movesZneg = false;
            }
        }
    }

    private void CalculateVirtualVelocitys()
    {
        var moveVector = aimObject.transform.position - lastPosition;
        var velocity3D = (moveVector) / Time.deltaTime;
        virtualXvel = velocity3D.x;
        virtualYvel = velocity3D.y;
        virtualZvel = velocity3D.z;
        virtualYZSlope = Vector3.Angle(new Vector3(0, 0, 1), new Vector3(0, Math.Abs(moveVector.y), Math.Abs(moveVector.z)));

        lastPosition = aimObject.transform.position;
    }
}
