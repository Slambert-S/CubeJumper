using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : BasicPlatform
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.OnGameStateChange += getNeighbour;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= getNeighbour;
    }
    void Start()
    {
        base.DoInitialisation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getNeighbour(GameManager.GameState obj)
    {
        if(obj  == GameManager.GameState.EnvironementUpdate)
        {
            identifyNeighbour();
        }
    }
    public override void testDoAction()
    {
        Debug.Log("This bloc has no special effect ");
    }
}
