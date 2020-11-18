using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class NerveCreator
{

    [MenuItem("Component/Eviroment Communication/Nerve System/Nerve")]
    static void CreateNerve()
    {
        GameObject nerve = new GameObject();
        nerve.name = "Nerve";
        BoxCollider nerveCollider = nerve.AddComponent<BoxCollider>();
        Rigidbody nerveRigidbody = nerve.AddComponent<Rigidbody>();
        nerveRigidbody.isKinematic = true;
        nerveRigidbody.useGravity = false;
        NerveMain nerveMain = nerve.AddComponent<NerveMain>();
        nerveMain.myTriggerCollider = nerveCollider;
        nerveCollider.isTrigger = true;
    }
}
