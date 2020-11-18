using System.Collections;
using UnityEngine;

[System.Serializable]
public class AnimatorControlParameters
{

}

[System.Serializable]
public class AnimatorBoolParameter : AnimatorControlParameters
{
    public string parameterName;
    public bool parameterValue;
    public float speedDamp;
    public int hashId;
    public AnimatorBoolParameter()
    {
        this.parameterName = "";
        this.parameterValue = false;
    }

    public void NameToHashID()
    {
        hashId = Animator.StringToHash(this.parameterName);
    }

    public void SetAnimatorValue(Animator animator)
    {
        animator.SetBool(hashId, parameterValue);
    }
}

[System.Serializable]
public class AnimatorFloatParameter : AnimatorControlParameters
{
    public string parameterName;
    public float parameterValue;
    public float speedDamp;
    public int hashId;
    public AnimatorFloatParameter()
    {
        this.parameterName = "";
        this.parameterValue = 0f;
    }

    public void NameToHashID()
    {
        this.hashId = Animator.StringToHash(this.parameterName);
    }

    public void SetAnimatorValue(Animator animator)
    {
        animator.SetFloat(hashId, parameterValue);
    }
}

[System.Serializable]
public class AnimatorIntParameter : AnimatorControlParameters
{
    public string parameterName;
    public int parameterValue;
    public float speedDamp;
    public int hashId;
    public AnimatorIntParameter()
    {
        this.parameterName = "";
        this.parameterValue = 0;
    }

    public void NameToHashID()
    {
        hashId = Animator.StringToHash(this.parameterName);
    }

    public void SetAnimatorValue(Animator animator)
    {
        animator.SetInteger(parameterName, parameterValue);
    }
}

[System.Serializable]
public class AnimatorTriggerParameter : AnimatorControlParameters
{
    public string parameterName;
    public float speedDamp;
    public int hashId;
    public AnimatorTriggerParameter()
    {
        this.parameterName = "";
    }

    public void NameToHashID()
    {
        hashId = Animator.StringToHash(this.parameterName);
    }

    public void SetAnimatorValue(Animator animator)
    {
        animator.SetTrigger(parameterName);
    }
}

[System.Serializable] 
public class AnimatorStateInfo
{
    public int laynumber;
    public string stateName;
    public int hashId;

    public void NameToHashID()
    {
        hashId = Animator.StringToHash(this.stateName);
    }
}

[System.Serializable]
public class AnimatorTransitionInfo
{
    public int laynumber;
    public string trasitionName;
    public int hashId;

    public void NameToHashID()
    {
        hashId = Animator.StringToHash(this.trasitionName);
    }
}