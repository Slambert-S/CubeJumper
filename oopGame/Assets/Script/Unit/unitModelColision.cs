using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitModelColision : MonoBehaviour
{
    public bool touchedWater = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "water")
        {
            Debug.Log("touch water");
            touchedWater = true;
        }
    }

    public void touchedWaterUsed()
    {
        touchedWater = false;
    }
}
