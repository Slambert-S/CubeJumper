using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTraking : MonoBehaviour
{
    public static TurnTraking instance;
    public int nbTurnsinceStart { get; private set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    
    private void OnEnable()
    {
        GameManager.OnGameStateChange += updateTurnNumber;
       
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= updateTurnNumber;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateTurnNumber(GameManager.GameState state)
    {
        if (state == GameManager.GameState.PlayerTurn)
        {
            nbTurnsinceStart++;
        }
    }
}
