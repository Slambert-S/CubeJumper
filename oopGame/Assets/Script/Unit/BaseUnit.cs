using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(unitStat))]
public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    private UnitType typeOfUnit;

    [SerializeField]
    private unitStat statRef;
    private int mouvmentActionStat = 1;  //need to encapsulate
    private int mouvementActionAvailable;
    [SerializeField]
    private BasicPlatform platformPlayerStandingOn;

    [SerializeField]
    private GameObject UnitSkin;

    [Header("Debug Section")]
    public BasicPlatform blockToMoveTO;

    //TO do : to clean when refactoring the mouvement
    public bool moving = false;
    public bool setUpDone = false;
    public Vector3 targetedPosition;
    public float speed = 1;

    private void OnEnable()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
        UnitSkin = this.transform.GetChild(0).gameObject;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(GameManager.GameState obj)
    {

        if(obj == GameManager.GameState.PlayerTurn)
        {
            ResetPlayerMouvement();
            this.gameObject.GetComponent<BuffManager>().startOfTurn();
            UpdatePlayerMouvementUI();
        }
       // throw new NotImplementedException();
    }

    // Start is called before the first frame update
    private void Start()
    {
        mouvementActionAvailable = mouvmentActionStat;
        statRef = this.gameObject.GetComponent<unitStat>();
        UpdatePlayerMouvementUI();
    }

    private void Update()
    {
        if (moving)
        {
            moveUnit();
        }
    }


    /* Section for all the function that change the stat of Base Unit*/
    public void addBonusMouvement(int number)
   {
        mouvementActionAvailable += number;
   }

   public void changeHpValue(int value)
   {
        statRef.ChangeHP = value;
   }


    public virtual void MoveTo(BasicPlatform selectedPlatform) {

        if(UnitSkin == null)
        {
            UnitSkin = this.transform.GetChild(0).gameObject;
        }
        if(GameManager.Instance.State != GameManager.GameState.PlayerTurn)
        {
            return;
        }

        if (checkToMoveToNewBox(selectedPlatform)) //This is abstraction
        {
            if (platformPlayerStandingOn != null)
            {
                platformPlayerStandingOn.playerIsOnTop = false;
            }
            else
            {
                Debug.LogWarning("play does not have a reference to a platform it is standing on");
            }

            MoveUnitVariableUpdate(selectedPlatform);
           
            mouvementActionAvailable--;
            UpdatePlayerMouvementUI();
            CheckEndPlayerTurn();
        }
        else
        {
            Debug.Log("Debug-BaseUnit-MoveTo : PLayer can not move to that block");
            ResetPlayerMouvement();
        }
    
    }

    private bool checkToMoveToNewBox(BasicPlatform selectedPlatform)
    {
        if (selectedPlatform.CanGoOnTop())
        {
            //Debug.LogWarning("Inside theToMoveToNewbox first if");
            if (platformPlayerStandingOn != null)
            {
               // Debug.LogWarning("Inside theToMoveToNewbox second check if");
                if (selectedPlatform.checkIfBoxIsANeighbour(platformPlayerStandingOn.gameObject))
                {

                 //   Debug.Log("DEBUG-BaseUnit-MoveTo : is a neighbour");
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
        Debug.LogWarning("Did not pass the first check in checkToMoveTONewBox");
        return false;
    }

    private void MoveUnitVariableUpdate(BasicPlatform platfromToMoveTo)
    {
        debugJumpTest();
        
        // Debugging section
        targetedPosition = platfromToMoveTo.GetPositionOnTop().position;

        moving = true;

        ///
       // this.transform.position = platfromToMoveTo.GetPositionOnTop().position;
        platformPlayerStandingOn.playerIsOnTop = false;
        platformPlayerStandingOn.unitOnTopReference = null;
        platformPlayerStandingOn = platfromToMoveTo;
        platformPlayerStandingOn.playerIsOnTop = true;
        platformPlayerStandingOn.unitOnTopReference = this.gameObject.GetComponent<BaseUnit>();
        platformPlayerStandingOn.UnitMovedOnTop();
    }

    //To do : refactor the jump feature 
    private void debugJumpTest()
    {
        moving = true;
        Rigidbody rgbd = UnitSkin.GetComponent<Rigidbody>();
        rgbd.AddForce(Vector3.up * 3 ,ForceMode.Impulse);
        UnitSkin.GetComponent<Animator>().SetFloat("Speed_f", 0.26f);
        UnitSkin.GetComponent<Animator>().SetTrigger("Jump_trig");
        //UnitSkin.GetComponent<Animator>().SetBool("Jump_b", false);
    }
    
    // To do : refactor the move Unit
    private void moveUnit()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetedPosition, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, targetedPosition) < 0.001f)
        {
            // Swap the position of the cylinder.
            //targetedPosition *= -1.0f;
            transform.position = targetedPosition;
            moving = false;
            UnitSkin.GetComponent<Animator>().SetFloat("Speed_f", 0.0f);
        }
    }

    public virtual void PushUnit(BasicPlatform finalPlatform,direction direction )
    {
        //Instan move unit
        MoveUnitVariableUpdate(finalPlatform);


    }

    private bool CheckIfplayerHaveEnuphMovement()
    {
        if(mouvementActionAvailable > 0)
        {
            return true;
        }
        else
        {
            Debug.Log("DEBUG-BaseUnit-CheckIfplayerHaveEnuphMovement : player dont have enuph mouvement");
            return false;
        }
    }

    private void ResetPlayerMouvement()
    {
        mouvementActionAvailable = mouvmentActionStat;
        UpdatePlayerMouvementUI();
       // platformPlayerStandingOn.testDoAction();
    }

    private void UpdatePlayerMouvementUI()
    {
        if(typeOfUnit == UnitType.player)
        {
            textUiManager.Instance.updatePlayerMouvementUi(mouvementActionAvailable);
        }
    }
    private void CheckEndPlayerTurn()
    {
        if(GameManager.Instance.State == GameManager.GameState.GameEnded)
        {
            return;
        }
        if(mouvementActionAvailable == 0)
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.EnvironmentTurn);
    }

    public enum UnitType
    {
        player,
        AI
    }

    public enum direction { 
        Up,
        Right,
        Down,
        Left

    }

}
