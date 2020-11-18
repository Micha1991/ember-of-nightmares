using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class NerveHandlerMain : MonoBehaviour
{
    public List<GameObject> knownNerves;
    public List<NerveMain> knownNerveMains;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region own methods
    public void GetAllMyNerves()
    {
        if (CheckIfItsAttached())
        {
            knownNerves = new List<GameObject>();
            knownNerveMains = new List<NerveMain>();
            Transform masterParent = gameObject.transform.root;
            GetAllGameObjectsInChildren(masterParent);
        }
        else
        {
            knownNerves = new List<GameObject>();
            knownNerveMains = new List<NerveMain>();
        }
    }
    private bool CheckIfItsAttached()
    {
        if (gameObject.transform.root != gameObject.transform)
        {
            return true;
        }
        else return false;
    }
    private void GetAllGameObjectsInChildren(Transform masterParent)
    {
        foreach (Transform g in masterParent.GetComponentsInChildren<Transform>())
        {
            NerveMain nerveMain = g.gameObject.GetComponent<NerveMain>();
            if (nerveMain != null)
            {
                knownNerves.Add(g.gameObject);
                knownNerveMains.Add(nerveMain);
            }
        }
    }
    #endregion
}
