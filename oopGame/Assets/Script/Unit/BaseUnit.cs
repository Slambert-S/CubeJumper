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
    [SerializeField]
    private int mouvmentActionStat = 1;  //need to encapsulate
    private int mouvementActionAvailable;
    [SerializeField]
    private BasicPlatform platformPlayerStandingOn;

    [SerializeField]
    private GameObject UnitSkin;

    public bool moving { get; private set; } = false;
    private Vector3 targetedPlatfromPosition;
    [SerializeField]
    private float jumpSpeed = 5;

    [Header("Debug Section")]
    public BasicPlatform blockToMoveTO;

   

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

        //Call the moveUnitFunction only when moving is true; moving == true whan the unit need to move to reach a destination
        if (moving)
        {
            moveUnitOverTime();
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


    /// <summary>
    /// Function to handle all the logic to move a unit from one block to another one.
    /// </summary>
    /// <param name="selectedPlatform"></param>
    public virtual void MoveToBlock(BasicPlatform selectedPlatform) {

        if(UnitSkin == null)
        {
            UnitSkin = this.transform.GetChild(0).gameObject;
        }
        if(GameManager.Instance.State != GameManager.GameState.PlayerTurn)
        {
            return;
        }
        //Block the player from moving again before finishing the moving animation.
        if (moving)
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
            Debug.Log("Debug-BaseUnit-MoveToBlock : PLayer can not move to that block");
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

                 //   Debug.Log("DEBUG-BaseUnit-MoveToBlock : is a neighbour");
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
        //Jump animation setUp;
        JumpAnimation();
        targetedPlatfromPosition = platfromToMoveTo.GetPositionOnTop().position;
        moving = true;

        platformPlayerStandingOn.playerIsOnTop = false;
        platformPlayerStandingOn.unitOnTopReference = null;
        platformPlayerStandingOn = platfromToMoveTo;
        platformPlayerStandingOn.playerIsOnTop = true;
        platformPlayerStandingOn.unitOnTopReference = this.gameObject.GetComponent<BaseUnit>();
        platformPlayerStandingOn.UnitMovedOnTop();
    }

    //To do : refactor the jump feature 
    private void JumpAnimation()
    {
        moving = true;
        Rigidbody rgbd = UnitSkin.GetComponent<Rigidbody>();
        rgbd.AddForce(Vector3.up * 3 ,ForceMode.Impulse);
        //UnitSkin.GetComponent<Animator>().SetFloat("Speed_f", 0.26f);
        
        UnitSkin.GetComponent<Animator>().SetTrigger("Jump_trig");
        //UnitSkin.GetComponent<Animator>().SetBool("Jump_b", false);
    }
    
    // To do : refactor the move Unit
    /// <summary>
    /// When call every frame, the function will move the unit toward a targeted position. over time
    /// </summary>
    private void moveUnitOverTime()
    {
        var step = jumpSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetedPlatfromPosition, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, targetedPlatfromPosition) < 0.001f)
        {
            // Swap the position of the cylinder.
            //targetedPlatfromPosition *= -1.0f;
            transform.position = targetedPlatfromPosition;
            moving = false;
            UnitSkin.GetComponent<Animator>().SetFloat("Speed_f", 0.0f);
            targetedPlatfromPosition = Vector3.zero;
        }
    }

    //Force push a unit in one direction
    //To-do : add animation and mouvement over time
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
