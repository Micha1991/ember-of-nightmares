using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerComponentsHandler : MonoBehaviour, ISerializationCallbackReceiver
{
    public Rigidbody playerRB;
    public PlayerCharacteristics playerCharacteristics;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        GetPlayerComponents();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region own methods
    void GetPlayerComponents()
    {
        GetRigidbody();
        GetPlayerCharacteristics();
    }
    void GetRigidbody()
    {
        if (playerRB == null)
            playerRB = GetComponent<Rigidbody>();
    }
    void GetPlayerCharacteristics()
    {
        if (playerCharacteristics == null)
            Debug.Log("Please attach a playerCharacteristics Scriptable Object");
    }
    #endregion
}
