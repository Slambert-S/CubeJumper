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
            
            //Debug.Log(unitOnTopReference);
            BaseBuff newbuff = unitOnTopReference.gameObject.AddComponent<MouvementBuff>();
            unitOnTopReference.GetComponent<BuffManager>().addNewBuff(newbuff);
            if (audioSource != null)
            {
                audioSource.PlayOneShot(bonusSound);

                //unitOnTopReference.GetComponent<unitMaterielManager>().ActivateYellowShader();
                StartCoroutine("changeShader");
            }
        }
    }

    private IEnumerator changeShader()
    {
        unitOnTopReference.GetComponent<unitMaterielManager>().ActivateYellowShader();
        yield return new WaitForSeconds(1);
        unitOnTopReference.GetComponent<unitMaterielManager>().DeactivateYellowShader();
    }
}
