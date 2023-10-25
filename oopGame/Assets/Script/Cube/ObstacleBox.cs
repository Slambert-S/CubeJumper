using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBox : BasicPlatform
{
    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void testDoAction()
    {
        Debug.Log("This bloc has no special effect ");
    }
}
