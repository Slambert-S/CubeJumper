using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatfrom : BasicPlatform
{
    private int pushDistance = 2;
    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
    }

    // Update is called once per frame
    public override void testDoAction()
    {
        for(int i = 0; i< 4; i++)
        {
           
            if (neighbourList[i] == null)
            {
                continue;
            }

            if (neighbourList[i].GetComponent<BasicPlatform>().playerIsOnTop)
            {
                
                //use out to get multiple return info
                BasicPlatform locationAfterPush = neighbourList[i].GetComponent<BasicPlatform>().GetInfoBeforePushingPlayer(this.gameObject, i, pushDistance,out bool fellOf);
                neighbourList[i].GetComponent<BasicPlatform>().unitOnTopReference.PushUnit(locationAfterPush, (BaseUnit.direction)1);
                Debug.Log("This is the final place and fell of = "+ fellOf, locationAfterPush);
            }
        }
    }
}