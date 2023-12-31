using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraPostion : MonoBehaviour
{
    private PlayerAction playerControl;
    private Camera mainCamera;

    private InputAction CameraAngle;
    private bool singleClickControll = false;

    [SerializeField]
    private GameObject backCameraPosition;
    [SerializeField]
    private GameObject frontCameraPosition;
    [SerializeField]
    private GameObject leftCameraPosition;
    [SerializeField]
    private GameObject rightCameraPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        mainCamera = Camera.main;
        playerControl = new PlayerAction();
    }

    private void OnEnable()
    {
        CameraAngle = playerControl.Player.CameraMove;
        CameraAngle.Enable();
        CameraAngle.performed += CameraControlInput;
        CameraAngle.canceled += CameraControlInput;
    }

    private void OnDisable()
    {
        CameraAngle.performed -= CameraControlInput;
        CameraAngle.canceled -= CameraControlInput;
        CameraAngle.Disable();
        
    }

    private void CameraControlInput(InputAction.CallbackContext context)
    {
        Vector2 cameraValue = context.ReadValue<Vector2>();

        if (singleClickControll == false)
        {
            //Debug.Log("Switch camera to the one that is represented by " + "X : " + cameraValue.x + "| Y : " + cameraValue.y);
            moveCamera(cameraValue);
            singleClickControll = true;
        }
        else
        {
            if(cameraValue == Vector2.zero)
            {
                singleClickControll = false;
            }
        }

        
        //Debug.Log("X : " + cameraValue.x + "| Y : " + cameraValue.y);
    }

    public void moveCamera(Vector2 direction)
    {
        GameObject positionReference = null ;
        if(direction.x != 0) {
            if(direction.x == 1)
            {
                Debug.Log("Move the the right camera");

                if (rightCameraPosition == null)
                {
                    Debug.LogWarning("Right Camera position is not set in [cameraPosition]");
                    return;
                }
                positionReference = rightCameraPosition;

            }
            else 
            if(direction.x == -1){
                Debug.Log("Move to the left camera");
                if (leftCameraPosition == null)
                {
                    Debug.LogWarning("Left Camera position is not set in [cameraPosition]");
                    return;
                }
                positionReference = leftCameraPosition;

            }
        }
        else
        {
            if (direction.y == 1)
            {
                Debug.Log("Move to the front camera");
                if (frontCameraPosition == null)
                {
                    Debug.LogWarning("Front Camera position is not set in [cameraPosition]");
                    return;
                }
                positionReference = frontCameraPosition;

            }
            else
            if (direction.y == -1)
            {
                if (backCameraPosition == null)
                {
                    Debug.LogWarning("Back Camera position is not set in [cameraPosition]");
                    return;
                }
                Debug.Log("Move to the back camera");
                positionReference = backCameraPosition;

            }
        }

        if(positionReference != null)
        {
            mainCamera.transform.parent.position = positionReference.transform.localToWorldMatrix.GetPosition();
            Debug.Log(positionReference.transform.localRotation.y);
            Quaternion newRotation = positionReference.transform.rotation;

            mainCamera.transform.parent.rotation = positionReference.transform.rotation;
           
        }

        
    }

    


}
