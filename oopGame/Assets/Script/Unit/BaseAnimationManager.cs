using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimationManager : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doInitialisation()
    {
        if(this.gameObject.GetComponent<BaseUnit>().UnitSkin.GetComponent<Animator>() != null)
        {
            animator = this.gameObject.GetComponent<BaseUnit>().UnitSkin.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("No animator found");
        }

    }


    /*
     * All folowing function should be transformed into vitrual function for child object to
     * handled them as needed.
     * 
     * 
     */
    
    
    public void SetDeathAnimation()
    {
        animator.SetBool("Death_b", true);
    }
}
