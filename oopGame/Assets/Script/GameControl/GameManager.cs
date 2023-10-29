using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChange;
    [SerializeField]
    private GameObject mouvementBoxParent;
    private int environmentTurnTracker = 0;
    public int turnBetweenEnvironmentChange = 2;

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

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.PlayerTurn:
                //Reset the player turn
                HandlePlayerReset();
                break;
            case GameState.EnvironmentTurn:
                //Activate all effect
                HandleEnvironemnetTurn();
                break;
            case GameState.EnvironementUpdate:
                //shuffle the terrain
                HandleTerrainShuffle();
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    private void HandleTerrainShuffle()
    {
      //  mouvementBoxParent.GetComponent<ChangeAllCube>().changeAllCube();

        //throw new NotImplementedException();
    }

    private void HandleEnvironemnetTurn()
    {
        if(mouvementBoxParent != null)
        {
            environmentTurnTracker++;
            int i = 0;

            foreach (Transform cube in mouvementBoxParent.transform)
            {
                if (i >= 50)
                {
                    break;
                }
                cube.GetComponentInChildren<BasicPlatform>().testDoAction();
                i++;
                Debug.Log("in the changeAllCube");
            }
            StartCoroutine(TurnChangeDelay());
            /*
            if (checkEnvironmentTurn())
            {
                environmentTurnTracker = 0;
                UpdateGameState(GameState.EnvironementUpdate);
            }
            else
            {
                UpdateGameState(GameState.PlayerTurn);
            };
            */
        }

   
        
    }
    IEnumerator TurnChangeDelay()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started of Environment turn at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        if (checkEnvironmentTurn())
        {
            environmentTurnTracker = 0;
            UpdateGameState(GameState.EnvironementUpdate);
        }
        else
        {
            UpdateGameState(GameState.PlayerTurn);
        };
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished of Environment turn at timestamp : " + Time.time);
    }


    private bool checkEnvironmentTurn()
    {
        if(environmentTurnTracker >= turnBetweenEnvironmentChange)
        {
            return true;
        }
        
        return false;
    }
    private void HandlePlayerReset()
    {

    }

    public enum GameState
    {
        PlayerTurn,
        EnvironmentTurn,
        EnvironementUpdate
    }
}
