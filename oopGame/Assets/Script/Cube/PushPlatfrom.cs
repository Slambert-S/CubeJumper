using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatfrom : BasicPlatform
{
    private int pushDistance = 2;
    [SerializeField]
    private AudioClip pushSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
        audioSource = this.gameObject.GetComponent<AudioSource>();
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
                BaseUnit unitReference = neighbourList[i].GetComponent<BasicPlatform>().unitOnTopReference;
                if(audioSource != null)
                {
                    audioSource.PlayOneShot(pushSound);
                }
                unitReference.PushUnit(locationAfterPush, (BaseUnit.direction)i);
                if (fellOf)
                {
                    unitReference.changeHpValue(-2);
                }
                Debug.Log("This is the final place and fell of = "+ fellOf, locationAfterPush);
            }
        }
    }
}
