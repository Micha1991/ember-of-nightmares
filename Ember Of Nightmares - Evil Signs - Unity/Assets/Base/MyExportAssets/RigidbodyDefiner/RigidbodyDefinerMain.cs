using System;
using UnityEngine;

public class RigidbodyDefinerMain : MonoBehaviour
{
    public Rigidbody definerRigidbody;
    public Animator animator;

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

    [SerializeField] AnimatorFloatParameter rigidbodySpeed = new AnimatorFloatParameter();

    // Start is called before the first frame update
    void Start()
    {
        rigidbodySpeed.NameToHashID();
    }

    // Update is called once per frame
    void Update()
    {
        SetRBStates();
        SetAnimatorParameters();
    }

    private void FixedUpdate()
    {
        CalculateVirtualVelocitys();
    }

    void SetRBStates()
    {
        if (definerRigidbody != null)
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
        var moveVector = definerRigidbody.transform.position - lastPosition;
        var velocity3D = (moveVector) / Time.deltaTime;
        virtualXvel = velocity3D.x;
        virtualYvel = velocity3D.y;
        virtualZvel = velocity3D.z;
        virtualYZSlope = Vector3.Angle(new Vector3(0, 0, 1), new Vector3(0, Math.Abs(moveVector.y), Math.Abs(moveVector.z)));
        rigidbodySpeed.parameterValue = CalculateRigidbodySpeed();

        lastPosition = definerRigidbody.transform.position;
    }

    float CalculateRigidbodySpeed()
    {
        return new Vector2(virtualXvel, virtualZvel).magnitude;
    }

    private void SetAnimatorParameters()
    {
        rigidbodySpeed.SetAnimatorValue(animator);
    }
}
