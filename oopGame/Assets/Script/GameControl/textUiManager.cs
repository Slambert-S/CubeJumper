using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class textUiManager : MonoBehaviour
{

    public static textUiManager Instance;
    public TMP_Text turnIndicator;
    public TMP_Text playerMouvementIndicator;
    public TMP_Text turnNumberIndicator;
    public Color PlayerTurnColour;
    public Color EnvironmentTurnColour;
    public Color EnvironmentChangeColour;
    public Color GameEndedcolour;


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
                // change colour

                break;
        }
    }

    public void updatePlayerMouvementUi(int nbMouvement)
    {
        playerMouvementIndicator.text = "Mouvement : " + nbMouvement;
    }

    public void updateTurnBeforeChange(int numberTurn)
    {
        turnNumberIndicator.text = ""+numberTurn;
    }

}
