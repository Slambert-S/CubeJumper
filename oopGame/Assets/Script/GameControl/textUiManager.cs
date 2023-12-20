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
                turnIndicator.text = "Player turn";
                turnIndicator.color = PlayerTurnColour;
                //change colour
                //reset  mouvement

                break;
            case GameManager.GameState.EnvironmentTurn:
                Debug.Log("End of player turn");
                // Change to Encironement turn
                turnIndicator.text = "Environment turn";
                turnIndicator.color = EnvironmentTurnColour;
                //change coulour

                break;
            case GameManager.GameState.EnvironementUpdate:
                //change to shuffeling
                turnIndicator.text = "Envirnment changing";
                turnIndicator.color = EnvironmentChangeColour;
                // change colour

                break;
            case GameManager.GameState.GameEnded:
                //change to shuffeling
                turnIndicator.text = "Game Ended";
                turnIndicator.color = EnvironmentChangeColour;
                GameEndedCanvasRef.SetActive(true);

                switch (GameManager.Instance.endOfGameManager.endReason)
                {
                    case GM_endOfGame.GameEndedState.GoalReached:
                        endOfGameMessage.text = "You saved your partner";
                        break;
                    case GM_endOfGame.GameEndedState.PlayerDeath:
                        endOfGameMessage.text = "You died";
                        break;
                }

                // change colour

                break;
        }
    }

    public void updatePlayerMouvementUi(int nbMouvement)
    {
        playerMouvementIndicator.text = "Mouvement : " + nbMouvement;
    }

    public void updatePlayerHpUi(int hpValue)
    {
        playerHpIndicator.text = "HP : " + hpValue;
    }

    public void updateTurnBeforeChange(int numberTurn)
    {
        turnNumberIndicator.text = ""+numberTurn;
    }

}
