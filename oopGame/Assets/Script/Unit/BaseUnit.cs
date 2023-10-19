using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    private int mouvmentSpeed = 1;  //need to encapsulate
    [SerializeField]
    private BasicPlatform platformPlayerStandingOn;

    [Header("Debug Section")]
    public BasicPlatform blockToMoveTO;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveTo(blockToMoveTO);
        }
    }

    public virtual void MoveTo(BasicPlatform selectedPlatform) {

        if (checkToMoveToNewBox(selectedPlatform)) //This is abstraction
        {
            this.transform.position = selectedPlatform.GetPositionOnTop().position;
            platformPlayerStandingOn = selectedPlatform;
        }
        else
        {
            Debug.Log("Debug-BaseUnit-MoveTo : PLayer can not move to that block");
        }
    
    }

    private bool checkToMoveToNewBox(BasicPlatform selectedPlatform)
    {
        if (selectedPlatform.CanGoOnTop())
        {
            if (platformPlayerStandingOn != null)
            {
                if (selectedPlatform.checkIfBoxIsANeighbour(platformPlayerStandingOn.gameObject))
                {
                    Debug.Log("DEBUG-BaseUnit-MoveTo : is a neighbour");
                    if (CheckIfplayerHaveEnuphMovement())
                    {
                        return true;

                    }
                }
            }
            else
            {
                Debug.LogWarning("No platformStanding on");
                return true; //To-Do change it later to fix the starting platform
            }
        }

            return false;
    }

    private bool CheckIfplayerHaveEnuphMovement()
    {
        return true;
    }

}
