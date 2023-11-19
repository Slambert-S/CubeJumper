using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeAllCube : MonoBehaviour
{
    public List<GameObject> cubePrefab = new List<GameObject>();
    private PlayerAction playerClick;
    private InputAction debugClick;
    [SerializeField]
    private int numberOfBoxType = 0;
    // Start is called before the first frame update
    private void Awake()
    {

        playerClick = new PlayerAction();
        numberOfBoxType = cubePrefab.Count;
    }
  
  
    private void OnEnable()
    {
        // mouseClickAction.Enable();
        //mouseClickAction.performed += clickAction;

        debugClick = playerClick.Player.debugKey;
        debugClick.Enable();
        ///debugClick.performed += changeAllCube;
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;

    }

    private void OnDisable()
    {
        //mouseClickAction.performed -= clickAction;
        //mouseClickAction.Disable();
        ///debugClick.performed -= changeAllCube;
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
        debugClick.Disable();
    }


    private void GameManagerOnGameStateChange(GameManager.GameState obj)
    {

        if (obj == GameManager.GameState.EnvironementUpdate)
        {
            changeAllCube();
        }
        // throw new NotImplementedException();
    }


    /* private void changeAllCube(InputAction.CallbackContext context)
     {
         int i = 0;

         foreach (Transform cube in transform)
         {
             if (i >= 50)
             {
                 break;
             }
             cube.GetComponentInChildren<BasicPlatform>().ChangeCubeObject(cubePrefab[GetBoxType()]);
             i++;
             Debug.Log("in the changeAllCube");
         }

         foreach (Transform cube in transform)
         {
             cube.GetComponentInChildren<BasicPlatform>().identifyNeighbour();
             Debug.Log("I got my neighbour");
         }
     }*/


  


    private void changeAllCube()
    {
        //Debug.Log("In changeAllCube");
        StartCoroutine(changeAllCubeWithTimeDelay());
    }
    IEnumerator changeAllCubeWithTimeDelay()
    {
        //Print the time of when the function is first called.
       // Debug.Log("Started of shuffle turn at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);


        int i = 0;

        foreach (Transform cube in transform)
        {
            if (i >= 50)
            {
                break;
            }
            cube.GetComponentInChildren<BasicPlatform>().ChangeCubeObject(cubePrefab[GetBoxType()]);
            i++;
           // Debug.Log("in the changeAllCube");
        }

        foreach (Transform cube in transform)
        {
            cube.GetComponentInChildren<BasicPlatform>().identifyNeighbour();
           // Debug.Log("I got my neighbour");
        }

        yield return new WaitForSeconds(1);
        foreach (StartPlatform platform in GameObject.FindObjectsOfType<StartPlatform>())
        {
            platform.identifyNeighbour();
        };

        foreach(EndPlatform platform in GameObject.FindObjectsOfType<EndPlatform>())
        {
            platform.identifyNeighbour();
        }
        GameManager.Instance.UpdateGameState(GameManager.GameState.PlayerTurn);

        //After we have waited 5 seconds print the time again.
       // Debug.Log("Finished of Shuffle turn at timestamp : " + Time.time);
    }

    private int GetBoxType() //Select a random new block, some condition will have to be met for some type of block
    {
        return Random.Range(0, numberOfBoxType);
    }
}
