using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    public int turnDuration = 1;
    public int lifeSpan;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        lifeSpan = turnDuration;
    }

    public void updateLifeSpan()
    {
        lifeSpan--;
        Debug.Log("buff is activated");
        activateEffect();
    }
    public void removeTheBuff()
    {
        Destroy(this);
    }

    public virtual void activateEffect()
    {
        //this.gameObject.GetComponent<BaseUnit>().addBonusMouvement(1);
    }
}
