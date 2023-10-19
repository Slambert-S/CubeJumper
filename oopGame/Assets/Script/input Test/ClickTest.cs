using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickTest : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClickAction;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.performed += clickAction;

    }

    private void OnDisable()
    {
        mouseClickAction.performed -= clickAction;
        mouseClickAction.Disable();
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

                this.gameObject.GetComponent<BaseUnit>().MoveTo(hit.collider.gameObject.GetComponent<BasicPlatform>());
               

                
              //  Debug.Log("Player position "+transform.position) ;
                //Debug.Log("Object position " + target.transform.position);

                /*  Vector3 noZaxisPosition = new Vector3(target.transform.position.x, this.gameObject.transform.position.y, target.transform.position.z);
                  Vector3 directionToTarget = noZaxisPosition - this.transform.position ;
                  Vector3 newFacingDirection = new Vector3(directionToTarget.x, directionToTarget.y, this.transform.position.z);
                  Debug.Log(Vector3.Angle(noZaxisPosition, transform.forward));
                  this.transform.right = directionToTarget;*/


            }
            
        }
    }
}
