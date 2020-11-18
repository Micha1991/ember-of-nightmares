using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttacksFreeModeController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private UserInput userInput;
    [SerializeField] private GameObject player;

    [Header("Animator Settings")]
    [SerializeField] private int attackLayerNumber;
    [Range(0,1)]
    [SerializeField] private float attackLayerWeight;
    [SerializeField] private AnimatorFloatParameter attackNumber = new AnimatorFloatParameter();
    [SerializeField] private AnimatorTriggerParameter attackTrigger = new AnimatorTriggerParameter();

    [Header("Implementer Settings")]
    [SerializeField] private float attackModeEscapeTimeMax;
    [SerializeField] private bool rightTriggerReleased;

    //Helper Variables
    [SerializeField] private enum CombatMeeleAttacks
    {
        Attack1 = 0,
        Attack2 = 1,
    }
    [SerializeField] private Vector3 attackDirection;


    // Start is called before the first frame update
    void Start()
    {
        attackNumber.NameToHashID();
        attackTrigger.NameToHashID();
    }

    // Update is called once per frame
    void Update()
    {
        SetLayerWeight();
        DrawAttackirection();
    }

    void SetLayerWeight()
    {
        var rightTriggerValue = userInput.uiRightTrigger;
        if (rightTriggerValue > 0.3)
        {
            attackLayerWeight = 1;


            if (rightTriggerReleased)
            {
                chooseAttack();
                SetAnimatorParameter();
            }
            animator.SetLayerWeight(attackLayerNumber, attackLayerWeight);
        }

        if (rightTriggerValue < 0.05)
        {
            rightTriggerReleased = true;
        }
        else
            rightTriggerReleased = false;
    }

    void chooseAttack()
    {
        var randomInt = Random.Range(0, 1);
        attackNumber.parameterValue = randomInt;
    }

    void SetAnimatorParameter()
    {
        attackNumber.SetAnimatorValue(animator);
        attackTrigger.SetAnimatorValue(animator);
    }

    void DrawAttackirection()
    {
        //Z Komponente
        attackDirection.z = player.transform.forward.z;
        attackDirection.y = player.transform.up.normalized.y;

        attackDirection.x = userInput.uiLeftStickHorizontal;
        attackDirection.z = userInput.uiLeftStickVertical;
        attackDirection.y = 1;
        attackDirection += player.transform.forward;
        Vector3.Normalize(attackDirection);
        Debug.DrawLine(player.transform.position, attackDirection, Color.blue);
        
    }
}
