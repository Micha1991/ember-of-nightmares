using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurvesSettings : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private CapsuleCollider mainCollider;

    [Header("Curve Names")]
    [SerializeField] private string mainColliderHeightCurveName;

    private float mainColliderStandardHeight;

    // Start is called before the first frame update
    void Start()
    {
        mainColliderStandardHeight = mainCollider.height;
    }

    // Update is called once per frame
    void Update()
    {
        SetMainColliderHeight();
    }

    void SetMainColliderHeight()
    {
        var mainColliderCurveHeight = animator.GetFloat(mainColliderHeightCurveName);
        if (mainColliderCurveHeight != 0)
            mainCollider.height = mainColliderStandardHeight * mainColliderCurveHeight;
        else if(mainCollider.height != mainColliderStandardHeight)
            mainCollider.height = mainColliderStandardHeight;
    }
}
