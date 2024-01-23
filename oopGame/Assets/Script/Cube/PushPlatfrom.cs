using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritence Child
public class PushPlatfrom : BasicPlatform
{
    private int pushDistance = 2;
    [SerializeField]
    private AudioClip pushSound;
    private AudioSource audioSource;
    [SerializeField]
    private ParticleSystem pushParticle;
    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        pushParticle = this.transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    //Polymorphism Child
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
                Vector3 particleDirection = new Vector3();
                switch (i)
                {
                    case 0:
                        particleDirection = new  Vector3(-180, 180, 0);
                        break;
                    case 1:
                        particleDirection = new Vector3(-180, -90, 0);
                        break;
                    case 2:
                        particleDirection = new Vector3(-180, 0, 0);
                        break;
                    case 3:
                        particleDirection = new Vector3(-180, 90, 0);
                        break;
                    default:
                        break;
                }

                pushParticle.transform.eulerAngles = particleDirection;
                pushParticle.Play();
                // faling overbord == true;
                
                unitReference.PushUnit(locationAfterPush, (BaseUnit.direction)i);
                if (fellOf)
                {
                    unitReference.fallingOverboard = true;
                  //  unitReference.transform.GetChild(1).gameObject.SetActive(true);
                    unitReference.changeHpValue(-2);
                }
                Debug.Log("This is the final place and fell of = "+ fellOf, locationAfterPush);
            }
        }
    }
}
