using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerRootController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public bool haveTarget;
    public GameObject target;
    public float angleToTarget;
    public bool userWantMove;
    public Vector3 lookTargetDir;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTarget();
        RefreshAngle();
        CheckUserInput();
        SetAnimatorVals();
        RotateToAngle();
        Jump();
    }
    private void FixedUpdate()
    {
        //implementet
        if(rb.angularVelocity.y != 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    //implented
    void GetTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //ToDo: Only make target if its kind of enemy or something else
                target = hit.transform.gameObject;
                if (!(LayerMask.LayerToName(target.layer) == "Enemy"))
                {
                    target = null;
                    haveTarget = false;
                }
                else
                {
                    haveTarget = true;
;
                }

            }
        }
    }

    void RefreshAngle()
    {
        if(target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            angleToTarget = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
            animator.SetFloat("Angle", angleToTarget);
            targetDir.y = 0;
            lookTargetDir = targetDir;
            Vector3 camFordward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camFordward.y = 0f;
            camRight.y = 0f;
            camFordward = camFordward.normalized;
            camRight = camRight.normalized;

            Vector3 desiredMoveDir = camFordward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal");
            //animator.SetFloat("InputX", desiredMoveDir.x);
            //animator.SetFloat("InputZ", desiredMoveDir.z);
            animator.SetFloat("InputX", Input.GetAxis("Horizontal"));
            animator.SetFloat("InputZ", Input.GetAxis("Vertical"));
            //animator.SetFloat("InputX", transform.forward.normalized.x * Input.GetAxis("Horizontal"));
            //animator.SetFloat("InputZ", transform.forward.normalized.y * Input.GetAxis("Vertical"));
        }
        else if (target == null)
        {
            Vector3 camFordward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camFordward.y = 0f;
            camRight.y = 0f;
            camFordward = camFordward.normalized;
            camRight = camRight.normalized;

            Vector3 desiredMoveDir = camFordward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal");
            angleToTarget = Vector3.SignedAngle(desiredMoveDir, transform.forward, Vector3.up);
            animator.SetFloat("Angle", angleToTarget);
            lookTargetDir = desiredMoveDir;
            animator.SetFloat("InputX", Input.GetAxis("Horizontal"));
            animator.SetFloat("InputZ", Input.GetAxis("Vertical"));
        }

        //animator.SetFloat("InputXRel", lookTargetDir.x * Input.GetAxis("Horizontal"));
        //animator.SetFloat("InputZRel", lookTargetDir.z * Input.GetAxis("Vertical"));
    }
    void RotateToAngle()
    {
        Vector3 lookTargetRound = new Vector3(Mathf.Round(lookTargetDir.x), Mathf.Round(lookTargetDir.y), Mathf.Round(lookTargetDir.z));
        if(lookTargetRound != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTargetDir), 0.5f);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

    }

    void SetAnimatorVals()
    {
        animator.SetBool("UserWantMove", userWantMove);
        animator.SetBool("HaveTarget", haveTarget);
    }

    void CheckUserInput()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            userWantMove = true;
        }
        else
            userWantMove = false;
    }

    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            animator.SetTrigger("Jump");
        }
    }
}
