using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
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
        }
       // throw new NotImplementedException();
    }

    // Start is called before the first frame update
    private void Start()
    {
        mouvementActionAvailable = mouvmentActionStat;
    }

    // Update is called once per frame
    void Update()
    {
       
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

            this.transform.position = selectedPlatform.GetPositionOnTop().position;
            platformPlayerStandingOn = selectedPlatform;
            platformPlayerStandingOn.playerIsOnTop = true;
            mouvementActionAvailable--;
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
       // platformPlayerStandingOn.testDoAction();
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
}
