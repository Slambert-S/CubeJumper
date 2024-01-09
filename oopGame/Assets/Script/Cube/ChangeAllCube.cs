using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public class SpawnValue
{
    public GameObject spawnObject;
    public float minPobabilityValue;
    public float maxProbabilityValue;
}

public class ChangeAllCube : MonoBehaviour
{
    public List<GameObject> cubePrefab = new List<GameObject>();
    public SpawnValue[] cubePrefabWithRating ;
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

    public void debugChangeaAllCube()
    {
        changeAllCube();
    }

  


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
        
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            yield return new WaitForSeconds(2);
            BoxGenerationLogic.Instance.resetValue();
        }
        else
        {
            yield return new WaitForSeconds(0);
        }

#endif
        

        int i = 0;

        foreach (Transform cube in transform)
        {
            if (i >= 100)
            {
                break;
            }
            int newBoxTypeIndex = GetBoxType();
            cube.GetComponentInChildren<BasicPlatform>().ChangeCubeObject(cubePrefabWithRating[newBoxTypeIndex].spawnObject,(cubeTypeController.CubeType) newBoxTypeIndex);
            i++;
           // Debug.Log("in the changeAllCube");
        }

        foreach (Transform cube in transform)
        {
            cube.GetComponentInChildren<BasicPlatform>().identifyNeighbour();
           // Debug.Log("I got my neighbour");
        }
        
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            yield return new WaitForSeconds(1);
        }

#endif
        

        foreach (StartPlatform platform in GameObject.FindObjectsOfType<StartPlatform>())
        {
            platform.identifyNeighbour();
        };

        foreach(EndPlatform platform in GameObject.FindObjectsOfType<EndPlatform>())
        {
            platform.identifyNeighbour();
        }

        
//#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.PlayerTurn);
        }
        
//#endif
        
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished of Shuffle turn at timestamp : " + Time.time);
        yield return null;
    }

    private int GetBoxType() //Select a random new block, some condition will have to be met for some type of block
    {
        
//#if UNITY_EDITOR
        int boxIndex = 0;
        bool boxTypeIsAvailable = false;
        int infiniteLoopPrevention = 0;
        do
        {
            infiniteLoopPrevention++;
            if (Application.isPlaying)
            {
                boxIndex = getCustumProbability();
                boxTypeIsAvailable = BoxGenerationLogic.Instance.checkBoxAvalability((cubeTypeController.CubeType)boxIndex);
            }
            else
            {
                Debug.Log("Custom probability result : " + getCustumProbability());
                boxIndex = getCustumProbability();
                boxTypeIsAvailable = this.GetComponent<BoxGenerationLogic>().checkBoxAvalability((cubeTypeController.CubeType)boxIndex);
                Debug.Log(boxIndex);
            }
           // boxTypeIsAvailable = BoxGenerationLogic.Instance.checkBoxAvalability((cubeTypeController.CubeType)boxIndex);
            if(infiniteLoopPrevention >= 100)
            {
                boxTypeIsAvailable = true;
                boxIndex = 0; // The Game will never be block if a basic bloc is added in extra
            }
        } while (boxTypeIsAvailable == false);
        

        return boxIndex;
        
//#endif
        
  //      return 0;
    }

    private int getCustumProbability()
    {
        float number = Random.Range(0.0f, 100.0f);
        int i  = 0;
        foreach(SpawnValue boxTypeRef in cubePrefabWithRating)
        {
            if(number >= boxTypeRef.minPobabilityValue && number <= boxTypeRef.maxProbabilityValue)
            {
                return i;
            }
            i++;
        }

        return 0;
    }
}


