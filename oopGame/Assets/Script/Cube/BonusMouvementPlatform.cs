using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMouvementPlatform : BasicPlatform
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip bonusSound;
    private AudioSource audioSource;
    void Start()
    {
        base.DoInitialisation();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    

    public override void testDoAction()
    {
       
        if (playerIsOnTop == true)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(bonusSound);
            }
            //Debug.Log(unitOnTopReference);
            BaseBuff newbuff = unitOnTopReference.gameObject.AddComponent<MouvementBuff>();
            unitOnTopReference.GetComponent<BuffManager>().addNewBuff(newbuff);
        }
    }
}
