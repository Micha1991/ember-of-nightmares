using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NerveGroundCreator
{
    [UnityEditor.MenuItem("Component/Eviroment Communication/Nerve System/Ground Nerve")]
    static void CreateNerve()
    {
        GameObject nerve = new GameObject();
        nerve.name = "Ground Nerve";
        BoxCollider nerveCollider = nerve.AddComponent<BoxCollider>();
        Rigidbody nerveRigidbody = nerve.AddComponent<Rigidbody>();
        nerveRigidbody.isKinematic = true;
        nerveRigidbody.useGravity = false;
        NerveGroundCheck nerveGroundCheck = nerve.AddComponent<NerveGroundCheck>();
        nerveGroundCheck.groundCollider = nerveCollider;
        nerveCollider.isTrigger = true;
    }
}
