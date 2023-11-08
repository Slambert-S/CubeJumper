using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    private UnitType typeOfUnit;
    private int mouvmentActionStat = 1;  //need to encapsulate
    private int mouvementActionAvailable;
    [SerializeField]
    private BasicPlatform platformPlayerStandingOn;

    [Header("Debug Section")]
    public BasicPlatform blockToMoveTO;

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
        UpdatePlayerMouvementUI();
    }


    /* Section for all the function that change the stat of Base Unit*/
   public void addBonusMouvement(int number)
   {
        mouvementActionAvailable += number;
   }


    public virtual void MoveTo(BasicPlatform selectedPlatform) {

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
        this.transform.position = platfromToMoveTo.GetPositionOnTop().position;
        platformPlayerStandingOn.playerIsOnTop = false;
        platformPlayerStandingOn.unitOnTopReference = null;
        platformPlayerStandingOn = platfromToMoveTo;
        platformPlayerStandingOn.playerIsOnTop = true;
        platformPlayerStandingOn.unitOnTopReference = this.gameObject.GetComponent<BaseUnit>();
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
