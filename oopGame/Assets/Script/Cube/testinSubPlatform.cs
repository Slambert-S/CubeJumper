using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testinSubPlatform : BasicPlatform
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
        Debug.Log("sub box did an action");

        /*if (playerIsOnTop == true)
        {
            Debug.Log(unitOnTopReference);
            BaseBuff newbuff = unitOnTopReference.gameObject.AddComponent<BaseBuff>();
            unitOnTopReference.GetComponent<BuffManager>().addNewBuff(newbuff);
        }*/
    }
}
