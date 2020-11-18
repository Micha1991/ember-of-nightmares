using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class NerveGroundCheck : MonoBehaviour
{
    public LayerMask whatIsGround;
    public bool isOnGround;
    [TagSelector] private string TagFilter = "";
    [TagSelector] public string[] useTag = new string[] { };
    [NonSerialized] public BoxCollider groundCollider;

    public bool useFootClip;
    public bool doFootClip;
    public AvatarIKGoal thisFoot;
    [Range(0, 2)]
    public float distanceToGround;

    private void OnEnable()
    {
        groundCollider = GetComponent<BoxCollider>();
        groundCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(!isOnGround)
            isOnGround = CheckIfLayerAndTagsAreIn(other.gameObject.layer, other.tag, whatIsGround, useTag);
        if (isOnGround && useFootClip)
            doFootClip = true;
        else
            doFootClip = false;
    }

    private void OnTriggerExit(Collider other)
    {
        //If we leave something that have defined layer and tags, set to false
        if(CheckIfLayerAndTagsAreIn(other.gameObject.layer, other.tag, whatIsGround, useTag))
            isOnGround = false;
    }

    bool CheckIfLayerAndTagsAreIn(int layernumber, string tagname, LayerMask layerMask, string[] tags)
    {
        var isIn = false;
        if (layerMask == (layerMask | (1 << layernumber)))
        {
            isIn = true;
            //Check if tag is one of selected tags
            if (useTag.Length > 0)
                if (!useTag.Contains(tagname))
                    isIn = false;
        }
        return isIn;
    }

    public void ClipFootOnGround(Animator animator)
    {
        //check if right or left foot is selected
        if (thisFoot == AvatarIKGoal.RightHand || thisFoot == AvatarIKGoal.LeftHand)
        {
            Debug.Log("You did not select a foot. Please select Left Foot, or Right Foot");
            return;
        }

        animator.SetIKPositionWeight(thisFoot, 1f);
        animator.SetIKRotationWeight(thisFoot, 1f);

        RaycastHit hit;
        Ray ray = new Ray(animator.GetIKPosition(thisFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, distanceToGround + 1f, whatIsGround))
        {
            if (useTag.Contains(hit.transform.tag))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;
                animator.SetIKPosition(thisFoot, footPosition);

                //animator.SetIKRotation(thisFoot, )
            }
        }

    }
    void ReleaseFootFromGround()
    {

    }

    private void Update()
    {

    }
}
