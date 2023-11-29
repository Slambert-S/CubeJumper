using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClick : MonoBehaviour
{
    private PlayerAction playerControl;
    private Camera mainCamera;

    private InputAction click;
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
        click.Enable();
        click.performed += clickAction;

    }

    private void OnDisable()
    {
        //mouseClickAction.performed -= clickAction;
        //mouseClickAction.Disable();
        click.performed -= clickAction;
        click.Disable();
    }

    private void clickAction(InputAction.CallbackContext context)
    {
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
}
