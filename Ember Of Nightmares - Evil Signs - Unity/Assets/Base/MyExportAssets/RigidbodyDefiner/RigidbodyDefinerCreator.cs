using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RigidbodyDefinerCreator
{
    [MenuItem("Component/Eviroment Communication/Rigidbody Definer")]
    static void CreateRigidbodyDefiner()
    {
        GameObject rigidbodyDefiner = new GameObject();
        rigidbodyDefiner.name = "Rigidbody Definer";
        //Rigidbody rb = rigidbodyDefiner.AddComponent<Rigidbody>();
        //rb.mass = 80f;
        //rb.useGravity = true;
        //rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
        RigidbodyDefinerMain rigidbodyDefinerMain = rigidbodyDefiner.AddComponent<RigidbodyDefinerMain>();
        //rigidbodyDefinerMain.definerRigidbody = rb;
    }
}
