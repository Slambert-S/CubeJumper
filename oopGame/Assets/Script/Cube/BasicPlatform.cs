using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> neighbourList = new List<GameObject>(4);
    [SerializeField]
    private Transform PositionReference;
    [SerializeField]
    private bool CanBeWalkedON = true;

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

    public  bool CanGoOnTop()
    {
        return CanBeWalkedON;
    }

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

    public void ChangeCubeObject(GameObject newBox)
    {
        if (!playerIsOnTop)
        {
            Instantiate(newBox, this.transform.position, this.transform.rotation, this.transform.parent.transform.parent);
            GameObject.Destroy(this.gameObject.transform.parent.gameObject);
        }

    }

}
