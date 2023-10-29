using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingChangeCube : MonoBehaviour
{

    public List<GameObject> cubePrefab = new List<GameObject>();
    private PlayerAction playerClick;
    private InputAction debugClick;
    // Start is called before the first frame update
    private void Awake()
    {
       
        playerClick = new PlayerAction();
    }

    private void OnEnable()
    {
        // mouseClickAction.Enable();
        //mouseClickAction.performed += clickAction;

        debugClick = playerClick.Player.debugKey;
        debugClick.Enable();
        debugClick.performed += changeAllCube;

    }

    private void OnDisable()
    {
        //mouseClickAction.performed -= clickAction;
        //mouseClickAction.Disable();
        debugClick.performed -= changeAllCube;
        debugClick.Disable();
    }
    private void changeAllCube(InputAction.CallbackContext context)
    {
        int i = 0;

        foreach(Transform cube in transform)
        {
            if(i >= 50)
            {
                break;
            }
            cube.GetComponentInChildren<BasicPlatform>().ChangeCubeObject(cubePrefab[0]);
            i++;
            Debug.Log("in the changeAllCube");
        } 

        foreach(Transform cube in transform)
        {
            cube.GetComponentInChildren<BasicPlatform>().identifyNeighbour() ;
            Debug.Log("I got my neighbour");
        }
    }
}
