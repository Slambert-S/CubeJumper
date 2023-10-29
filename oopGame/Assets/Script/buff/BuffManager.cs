using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<BaseBuff> listOfbuff;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startOfTurn()
    {
        updateAllBuff();
    }

    private void updateAllBuff()
    {
        if(listOfbuff.Count >= 1)
        {
            foreach(BaseBuff buff in listOfbuff)
            {
                
                if(buff.lifeSpan > 0)
                {
                    buff.updateLifeSpan();
                    BaseBuff currentBuffRef = buff;
                    buff.removeTheBuff();
                }
            }

            listOfbuff.RemoveAll(x => !x);
        }
    }

    public void addNewBuff(BaseBuff buff)
    {
        listOfbuff.Add(buff);
    }
}
