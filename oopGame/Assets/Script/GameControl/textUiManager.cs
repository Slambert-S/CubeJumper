using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class textUiManager : MonoBehaviour
{

    public static textUiManager Instance;
    [SerializeField]
    private TMP_Text turnIndicator;
    [SerializeField]
    private TMP_Text playerMouvementIndicator;
    [SerializeField]
    private TMP_Text turnNumberIndicator;
    [SerializeField]
    private TMP_Text playerHpIndicator;
    public Color PlayerTurnColour;
    public Color EnvironmentTurnColour;
    public Color EnvironmentChangeColour;
    public Color GameEndedcolour;

    public GameObject GameEndedCanvasRef;
    public GameObject gameUIRef;
    [SerializeField]
    private TMP_Text endOfGameMessage;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUiState(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.PlayerTurn:
                //Change to player turn
                turnIndicator.text = "P";
                turnIndicator.color = PlayerTurnColour;
                //change colour
                //reset  mouvement

                break;
            case GameManager.GameState.EnvironmentTurn:
                Debug.Log("End of player turn");
                // Change to Encironement turn
                turnIndicator.text = "E";
                turnIndicator.color = EnvironmentTurnColour;
                //change coulour

                break;
            case GameManager.GameState.EnvironementUpdate:
                //change to shuffeling
                turnIndicator.text = "S";
                turnIndicator.color = EnvironmentChangeColour;
                // change colour

                break;
            case GameManager.GameState.GameEnded:
                //change to shuffeling
                StartCoroutine("reachEndPlatfromTime");
              /*  turnIndicator.text = "Game Ended";
                turnIndicator.color = EnvironmentChangeColour;
                
                if(gameUIRef == null)
                {
                    Debug.LogWarning("No reference to gameUI in textUiManager");
                }
                else
                {
                    gameUIRef.SetActive(false);
                }

                if (GameEndedCanvasRef == null)
                {
                    Debug.LogWarning("No reference to GameEndedCanvasRef in textUiManager");
                }
                else
                {
                    GameEndedCanvasRef.SetActive(true);
                }

                switch (GameManager.Instance.endOfGameManager.endReason)
                {
                    case GM_endOfGame.GameEndedState.GoalReached:
                        endOfGameMessage.text = "You saved your partner";
                        break;
                    case GM_endOfGame.GameEndedState.PlayerDeath:
                        endOfGameMessage.text = "You died";
                        break;
                }

                // change colour*/

                break;
        }
    }

    IEnumerator reachEndPlatfromTime()
    {
        yield return new WaitForSeconds(2);
        //Do animation
        turnIndicator.text = "Game Ended";
        turnIndicator.color = EnvironmentChangeColour;

        if (gameUIRef == null)
        {
            Debug.LogWarning("No reference to gameUI in textUiManager");
        }
        else
        {
            gameUIRef.SetActive(false);
        }

        if (GameEndedCanvasRef == null)
        {
            Debug.LogWarning("No reference to GameEndedCanvasRef in textUiManager");
        }
        else
        {
            GameEndedCanvasRef.SetActive(true);
        }

        switch (GameManager.Instance.endOfGameManager.endReason)
        {
            case GM_endOfGame.GameEndedState.GoalReached:
                endOfGameMessage.text = "You saved your partner";
                break;
            case GM_endOfGame.GameEndedState.PlayerDeath:
                endOfGameMessage.text = "You died";
                break;
        }

    }
    public void updatePlayerMouvementUi(int nbMouvement)
    {
        playerMouvementIndicator.text = "" + nbMouvement;
    }

    public void updatePlayerHpUi(int hpValue)
    {
        playerHpIndicator.text = "" + hpValue;
    }

    public void updateTurnBeforeChange(int numberTurn)
    {
        turnNumberIndicator.text = ""+numberTurn;
    }

}
