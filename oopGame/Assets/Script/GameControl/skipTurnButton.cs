using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipTurnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void skipPlayerTurn()
    {
        if(GameManager.Instance.State == GameManager.GameState.PlayerTurn)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.EnvironmentTurn);
        }
    }
}
