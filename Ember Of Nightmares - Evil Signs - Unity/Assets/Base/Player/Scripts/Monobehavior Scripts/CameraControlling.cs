using UnityEngine;

public class CameraControlling : MonoBehaviour
{
    #region Programmer Settings
    [Header("Required Components")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform player;
    [SerializeField] private UserInput userInput;
    #endregion

    [Header("User Prefs")]
    [SerializeField] private float cameraRotationsSpeed = 200f;

    #region Help Variables
    private Quaternion lastRotation;
    #endregion

    private void LateUpdate()
    {
        RotateCameraByController();
        lastRotation = followTarget.rotation;
    }

    void RotateCameraByController()
    {
        if(userInput.uiRightStickHorizontal != 0)
        {
            Quaternion rotationVector = Quaternion.Euler(0, userInput.uiRightStickHorizontal * Time.deltaTime * cameraRotationsSpeed, 0);
            followTarget.rotation *= rotationVector;
        }
        else
        {
            followTarget.rotation = lastRotation;
        }

    }
}
