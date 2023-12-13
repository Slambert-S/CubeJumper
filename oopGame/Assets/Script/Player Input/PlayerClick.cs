using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClick : MonoBehaviour
{
    private PlayerAction playerControl;
    private Camera mainCamera;

    private InputAction click;
    private InputAction draging;
    private void Awake()
    {
        mainCamera = Camera.main;
        playerControl = new PlayerAction();
    }

    private void OnEnable()
    {
        // mouseClickAction.Enable();
        //mouseClickAction.performed += clickAction;

        click = playerControl.Player.Clicked;
        draging = playerControl.Player.Drag;
        //playerControl.Player.debugKey.performed
        draging.Enable();
        click.Enable();
        draging.performed += testDraging;
        draging.canceled += testDraging;
        click.performed += clickAction;
        click.canceled += clickAction;

    }

    private void OnDisable()
    {
        //mouseClickAction.performed -= clickAction;
        //mouseClickAction.Disable();
        click.performed -= clickAction;
        click.canceled -= clickAction;
        draging.performed -= testDraging;
        draging.canceled -= testDraging;
        click.Disable();
        draging.Disable();
    }

    private void clickAction(InputAction.CallbackContext context)
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("OverGameObject");
            return;
        }

        if (mainCamera.gameObject.GetComponent<CameraDrag>().checkIfCameraMoved == true)
        {
            Debug.Log("Dont move");
            return;    
        }
        
       
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        

        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider)
        {
            //Debug.Log (hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<BasicPlatform>() && GameManager.Instance.State == GameManager.GameState.PlayerTurn)
            {
                GameObject target = hit.collider.gameObject.GetComponent<BasicPlatform>().gameObject;

                // Block the model from rotating while jumping from one block to the other
                if (this.gameObject.GetComponent<BaseUnit>().moving == false)
                {
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                }

                this.gameObject.GetComponent<BaseUnit>().MoveToBlock(hit.collider.gameObject.GetComponent<BasicPlatform>());

            }

        }
    }

    private void testDraging(InputAction.CallbackContext context)
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("OverGameObject");
            return;
        }
        
        mainCamera.gameObject.GetComponent<CameraDrag>().testDraging(context);
        Debug.Log(context.phase);
        //print the current mouse position on the playing field  as a vector 2
        //Debug.Log(context.ReadValue<Vector2>());

        
    }
}
