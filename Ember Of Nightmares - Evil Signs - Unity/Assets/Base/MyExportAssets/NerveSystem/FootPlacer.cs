using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FootPlacer : MonoBehaviour
{
    public Animator animator;
    public NerveGroundCheck rightFootNerveGroundCheck;
    public NerveGroundCheck leftFootNerveGroundCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(animator == null)
        {
            Debug.Log("You did not selected an animator. Please select the Animator from the Avatar you want to change foot orientation");
            return;
        }

        if (rightFootNerveGroundCheck.useFootClip)
        {
            rightFootNerveGroundCheck.ClipFootOnGround(animator);
        }
        if (leftFootNerveGroundCheck.doFootClip)
        {
            rightFootNerveGroundCheck.ClipFootOnGround(animator);
        }
    }

}
