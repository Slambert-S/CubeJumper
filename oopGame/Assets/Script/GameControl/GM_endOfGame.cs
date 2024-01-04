using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_endOfGame : MonoBehaviour
{
    public GameEndedState endReason;
    // Start is called before the first frame update
   

    public void executeEndOfGame()
    {
        switch (endReason)
        {
            case GameEndedState.GoalReached:
                Debug.Log("You reached the goal");
                break;
            case GameEndedState.PlayerDeath:
                Debug.Log("You are dead");

                break;
        }
        Time.timeScale = 0;

    }

    public enum GameEndedState
    {
        PlayerDeath,
        GoalReached 
    }

}
