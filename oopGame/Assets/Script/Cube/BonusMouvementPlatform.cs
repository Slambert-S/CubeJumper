using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMouvementPlatform : BasicPlatform
{
    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
    }

    

    public override void testDoAction()
    {
       
        if (playerIsOnTop == true)
        {
            Debug.Log(unitOnTopReference);
            BaseBuff newbuff = unitOnTopReference.gameObject.AddComponent<MouvementBuff>();
            unitOnTopReference.GetComponent<BuffManager>().addNewBuff(newbuff);
        }
    }
}
