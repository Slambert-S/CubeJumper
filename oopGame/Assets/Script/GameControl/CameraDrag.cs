using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraDrag : MonoBehaviour
{
   /* public float dragSpeed = 2;
    private Vector3 dragOrigin;*/
   [SerializeField]
    private Vector3 ResetCamera;
    [SerializeField]
    private Vector3 Origin;
    [SerializeField]
    private Vector3 Diference;

    public bool dragFirstCheck  = false;
    public bool dragSecondCheck = false;
    public bool checkIfCameraMoved = false;

    public Vector3 currentScreenPoint;
    public Vector3 oldScreenPoint;
    public Vector3 originalScreenPosition;

    public Vector3 dragOrigine;
    public Vector3 newDragPoint;

    public float dragSpeed=2;

    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }
    

    public void testDraging(InputAction.CallbackContext context)
    {

        //print the current mouse position on the playing field  as a vector 2
        //Debug.Log(context.ReadValue<Vector2>());

        if (context.phase ==InputActionPhase.Performed )
        {
         
            //Swap the previous frame mouse position
            if (newDragPoint != null && dragOrigine != newDragPoint)
            {
                dragOrigine = newDragPoint;           
            }

            // Called when the the drag first start
            // Set the drag to true and reset the dragOrigine
            if (dragFirstCheck == false)
            {
                dragFirstCheck = true;
                dragSecondCheck = true;
          
                dragOrigine = context.ReadValue<Vector2>();
                originalScreenPosition = context.ReadValue<Vector2>();
            }

        }
        else
        {
            dragSecondCheck = false;
            StartCoroutine(SmallDelayForDrag());
           
        }

        if (dragFirstCheck == true && dragSecondCheck == true )
        {
           // Debug.Log("I am suppose to move");
            newDragPoint = context.ReadValue<Vector2>();
            if((newDragPoint - originalScreenPosition).magnitude <= new Vector3(1, 1, 1).magnitude)
            {
                return;
            }
            checkIfCameraMoved = true;
            //This take the original position on the screen and compare it ti 
            Vector3 pos = Camera.main.ScreenToViewportPoint(newDragPoint - dragOrigine);
            Vector3 mouve = new Vector3(pos.x * dragSpeed * -1, 0, pos.y * dragSpeed * -1);

            //Vector3 mouve = new Vector3(pos.x * dragSpeed * -1, 0, 0);

            //The mouvement will be based on where the camera is looking;
            transform.parent.transform.Translate(mouve, Space.Self);

        }
 
    }
    IEnumerator SmallDelayForDrag()
    {
        //dragOrigine = context.ReadValue<Vector2>();
        yield return new WaitForEndOfFrame();
        dragFirstCheck = false;
        checkIfCameraMoved = false;
    }
    

}
