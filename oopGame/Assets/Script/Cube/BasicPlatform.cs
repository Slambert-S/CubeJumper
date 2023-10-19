using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField]
    private List<BasicPlatform> neighbourList = new List<BasicPlatform>(4);
    [SerializeField]
    private Transform PositionReference;

    private bool CanBeWalkedON = true;
    // Start is called before the first frame update
    void Start()
    {
        PositionReference = transform.parent.GetComponentInChildren<REF_centerPoint>().gameObject.transform;


        identifyNeighbour();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void identifyNeighbour()
    {
        for (int i = 0; i < 4 ; i++) //list initialisation
        {
            neighbourList.Add(null);
        }

        neighbourList[0] = CastRayCast(this.transform.forward);
        neighbourList[1] = CastRayCast(this.transform.right);
        neighbourList[2] = CastRayCast(this.transform.forward * -1);
        neighbourList[3] = CastRayCast(this.transform.right * -1);
        
    }

    public bool checkIfBoxIsANeighbour(GameObject boxToFind)
    {
        
        foreach(BasicPlatform neighbour in neighbourList)
        {
            if(neighbour!= null) //prevent calling error if the cube have an empty side
            {
                if (neighbour.gameObject.GetInstanceID() == boxToFind.gameObject.GetInstanceID())
                {
                    return true;
                }
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

    private BasicPlatform CastRayCast(Vector3 direction)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            if (hit.collider.gameObject.GetComponent<BasicPlatform>())
            {
                return hit.collider.gameObject.GetComponent<BasicPlatform>();
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
}
