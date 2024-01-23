using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickTest : MonoBehaviour
{
    /*
    [SerializeField]
    private InputAction mouseClickAction;
    */
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

        if(Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider)
        {
            //Debug.Log (hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<BasicPlatform>())
            {
                GameObject target = hit.collider.gameObject.GetComponent<BasicPlatform>().gameObject;
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

                this.gameObject.GetComponent<BaseUnit>().MoveToBlock(hit.collider.gameObject.GetComponent<BasicPlatform>());
               



            }
            
        }
    }
}
