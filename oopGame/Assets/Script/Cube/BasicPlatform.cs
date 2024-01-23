using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritence Parent
public class BasicPlatform : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> neighbourList = new List<GameObject>(4);
    [SerializeField]
    private Transform PositionReference;
    [SerializeField]
    private bool CanBeWalkedON = true;

    private cubeTypeController.CubeType currentCubeType = cubeTypeController.CubeType.Normal;

   // [SerializeField]
    public BaseUnit unitOnTopReference; // To-Do improve privacy

    public bool playerIsOnTop { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        DoInitialisation();


    }
    protected void DoInitialisation()
    {
        PositionReference = transform.parent.GetComponentInChildren<REF_centerPoint>().gameObject.transform;


        identifyNeighbour();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(GameManager.GameState obj)
    {

        if (obj == GameManager.GameState.EnvironmentTurn)
        {
            // ResetPlayerMouvement();
           // this.testDoAction();
        }
        // throw new NotImplementedException();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void identifyNeighbour()
    {
        if(neighbourList.Count < 1)
        {
            for (int i = 0; i < 4; i++) //list initialisation
            {
                neighbourList.Add(null);
            }
        }
        

        neighbourList[0] = CastRayCast(this.transform.forward);
        neighbourList[1] = CastRayCast(this.transform.right);
        neighbourList[2] = CastRayCast(this.transform.forward * -1);
        neighbourList[3] = CastRayCast(this.transform.right * -1);
        
    }

    /* 
     * This script check if the block the player is standing on is a neighbour of the block the player clicked on.
     * Make sur that the hitbox of all platfrom are long enuph.
     */
    public bool checkIfBoxIsANeighbour(GameObject boxToFind)
    {
        
        foreach(GameObject neighbour in neighbourList)
        {
           // Debug.LogWarning("Inside check for neighbout");
            if (neighbour!= null) //prevent calling error if the cube have an empty side
            {
                if (neighbour.gameObject.GetInstanceID() == boxToFind.gameObject.GetInstanceID())
                {
                    return true;
                }
            }
            else
            {
                Debug.LogWarning("No neighbour");
            }
            
        }
        return false;
    }

    /// Encapsulation
    public  bool CanGoOnTop()
    {
        return CanBeWalkedON;
    }
    /// Encapsulation
    public Transform GetPositionOnTop()
    {
        return PositionReference;
    }

    private GameObject CastRayCast(Vector3 direction)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            if (hit.collider.gameObject.GetComponent<BasicPlatform>())
            {
                return hit.collider.gameObject.GetComponent<BasicPlatform>().gameObject;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1000, Color.white);
            //Debug.Log("Did not Hit");
            return null;
        }
        return null;
    }

    public virtual void testDoAction()
    {
       // Debug.Log("Basic box did an action");
    }


    //Polymorphysm Parent
    /// <summary>
    /// Virtual methode : is use to trigger special action when player move on top of the platfrom.
    /// </summary>
    public virtual void UnitMovedOnTop()
    {

    }

    public BasicPlatform GetInfoBeforePushingPlayer(GameObject neighbour , int side , int level, out bool fellOf)
    {
     
        if(level > 0)
        {
            level--;
            if (neighbourList[side] == null)
            {
                Debug.Log("Player Would have fall");
                fellOf = true;
                return this;
              
            }
            if(neighbourList[side].GetComponent<BasicPlatform>().CanBeWalkedON == false)
            {
                Debug.Log("Player hit a rock");
                fellOf = false;
                return this;
            }
            // recursive call to the next neighbour
            BasicPlatform tempNeighbour = neighbourList[side].GetComponent<BasicPlatform>().GetInfoBeforePushingPlayer(neighbour, side, level, out fellOf);

            return tempNeighbour;
        }
        else
        {
            fellOf = false;
            return this;
        }
  
    }

    public void ChangeCubeObject(GameObject newBox, cubeTypeController.CubeType newCubeType)
    {
        bool fixType = this.transform.parent.transform.parent.GetComponent<cubeTypeController>().fixType;
#if UNITY_EDITOR
        if (!playerIsOnTop && fixType == false)
        {

            //To do  handle the generation logic when randomizing cube in editor mode
            if (BoxGenerationLogic.Instance)
            {
                BoxGenerationLogic.Instance.removeOldBoxType(currentCubeType);
                Instantiate(newBox, this.transform.position, this.transform.rotation, this.transform.parent.transform.parent);
                // GameObject.Destroy(this.gameObject.transform.parent.gameObject);
                DestroyImmediate(this.gameObject.transform.parent.gameObject);
                BoxGenerationLogic.Instance.AddNewBoxType(newCubeType);
                currentCubeType = newCubeType;
            }
            else
            {
                //Debug.LogWarning("BoxGenerationLogic has no valid Instance");
                
                BoxGenerationLogic refference = gameObject.transform.parent.parent.parent.GetComponent<BoxGenerationLogic>();
                refference.removeOldBoxType(currentCubeType);
                Instantiate(newBox, this.transform.position, this.transform.rotation, this.transform.parent.transform.parent);
                // GameObject.Destroy(this.gameObject.transform.parent.gameObject);
                DestroyImmediate(this.gameObject.transform.parent.gameObject);
                refference.AddNewBoxType(newCubeType);
                currentCubeType = newCubeType;
            }
            
        }
#else

        if (!playerIsOnTop && fixType == false)
        {
            Instantiate(newBox, this.transform.position, this.transform.rotation, this.transform.parent.transform.parent);
            GameObject.Destroy(this.gameObject.transform.parent.gameObject);
        }

#endif

    }

}
