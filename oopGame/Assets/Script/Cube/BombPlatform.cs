using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlatform : BasicPlatform
{
    [SerializeField]
    private int nbTurnUntilExplosion = 1;
    [SerializeField]
    private List<GameObject> listOfBombParticle = new List<GameObject>();

    [SerializeField]
    private int damageValue = 4;
    private ParticleSystem BigExplosionRef;

    [SerializeField]
    private bool hasExploded = false;

    [SerializeField]
    private AudioClip explosionSound;
    private AudioSource audioSourc;

    // Start is called before the first frame update
    void Start()
    {
        base.DoInitialisation();
        audioSourc = this.gameObject.GetComponent<AudioSource>();
        GameObject vfxParentRef = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        BigExplosionRef = this.gameObject.transform.GetChild(0).Find("bigExplosions").GetComponent<ParticleSystem>();
        int nbChilde = vfxParentRef.transform.childCount;
        for(int i = 0; i< nbChilde; i++)
        {
            listOfBombParticle.Add(vfxParentRef.transform.GetChild(i).gameObject);
        }
       
        setExplosionDelay();
    }

    private void setExplosionDelay()
    {
        
        int RandomNumberOfTurn = Random.Range(1, listOfBombParticle.Count); // Random.Range(minInclusive,maxExclusive) for int
        nbTurnUntilExplosion = RandomNumberOfTurn;

        //light up all the sparks that need to be on
        for(int i = listOfBombParticle.Count -1; i>= nbTurnUntilExplosion -1 ; i--)
        {
            listOfBombParticle[i].GetComponent<ParticleSystem>().Play();
        }
        //1  = turn befor explo  = explode at the next environement turn
        //2  = explode the one following this turn
        //3  =
    }

    private void updateNbOfBombLit()
    {
        listOfBombParticle[nbTurnUntilExplosion - 1].GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    public override void testDoAction()
    {
        UpdateSparks();
    }
    private void UpdateSparks() 
    {
        if(hasExploded == false)
        {
            if (nbTurnUntilExplosion == 1)
            {
                //Explode the bomb
                if (audioSourc != null)
                {
                    audioSourc.PlayOneShot(explosionSound);
                }

                BigExplosionRef.Play();
                hasExploded = true;
                GameObject parentOfBombVisual = this.gameObject.transform.GetChild(0).Find("bombVisual").gameObject;
                for (int i = 0; i < parentOfBombVisual.transform.childCount; i++)
                {
                    parentOfBombVisual.transform.GetChild(i).gameObject.SetActive(false);
                    listOfBombParticle[i].GetComponent<ParticleSystem>().Stop();
                }
                if (playerIsOnTop)
                {
                    unitOnTopReference.changeHpValue(-damageValue);
                }
                //ToDo - may change for a specific function call to handle visual feedback
            }
            else
            {
                nbTurnUntilExplosion--;
                updateNbOfBombLit();
            }
        }
       
    }
}
