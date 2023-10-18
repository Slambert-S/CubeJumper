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
            Debug.Log (hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<BasicPlatform>())
            {
                this.gameObject.GetComponent<BaseUnit>().MoveTo(hit.collider.gameObject.GetComponent<BasicPlatform>());
                GameObject target = hit.collider.gameObject.GetComponent<BasicPlatform>().gameObject;
                Vector3 directionToTarget = target.transform.position - this.transform.position ;
                Vector3 newFacingDirection = new Vector3(directionToTarget.x, directionToTarget.y, this.transform.position.z);
                Debug.Log(Vector3.Angle(target.transform.position, transform.forward));
               // this.transform.Rotate(0, this.transform.rotation.y+90, 0);
                
            }
            
        }
    }
}
