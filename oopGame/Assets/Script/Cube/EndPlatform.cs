using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : BasicPlatform
{
    private bool condition = true;
    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
    }

    // Update is called once per frame
    public override void testDoAction()
    {
        
    }

    public override void UnitMovedOnTop()
    {
        // Trigger end of game and switch game state to match
        if (this.playerIsOnTop)
        {
            if (condition == true) // To-Do update later to check for game win condition
            {
                Debug.Log("reached end platfrom");
                GameManager.Instance.UpdateGameState(GameManager.GameState.GameEnded);
            }
        }
    }
}
