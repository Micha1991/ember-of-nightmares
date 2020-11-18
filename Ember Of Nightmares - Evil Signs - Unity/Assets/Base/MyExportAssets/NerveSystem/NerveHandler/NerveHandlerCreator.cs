using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class NerveHandlerCreator
{

    [MenuItem("Component/Eviroment Communication/Nerve System/Nerve Handler")]
    static void CreateNerveHandler()
    {
        GameObject nerveHandler = new GameObject();
        nerveHandler.name = "Nerve Handler";
        nerveHandler.AddComponent<NerveHandlerMain>();
    }
}
